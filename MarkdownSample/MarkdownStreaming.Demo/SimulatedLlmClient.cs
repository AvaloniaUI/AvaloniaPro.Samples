using System.Runtime.CompilerServices;

namespace MarkdownStreaming.Demo;

/// <summary>
/// Simulates an LLM streaming response by emitting tokens from a
/// pre-defined markdown string at a configurable rate.
/// </summary>
internal sealed class SimulatedLlmClient
{
    private readonly string _content;
    private readonly int _tokensPerSecond;

    public SimulatedLlmClient(string markdownContent, int tokensPerSecond = 60)
    {
        _content = markdownContent;
        _tokensPerSecond = tokensPerSecond;
    }

    /// <summary>
    /// Streams the content as word-boundary tokens at the configured rate.
    /// Simulates realistic LLM token boundaries (roughly word-level).
    /// </summary>
    public async IAsyncEnumerable<string> StreamAsync(
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        int delayMs = Math.Max(1, 1000 / _tokensPerSecond);
        int pos = 0;

        while (pos < _content.Length)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;

            int tokenEnd = FindTokenEnd(pos);
            var token = _content.Substring(pos, tokenEnd - pos);
            pos = tokenEnd;

            yield return token;

            try
            {
                await Task.Delay(delayMs, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                yield break;
            }
        }
    }

    /// <summary>
    /// Finds the next token boundary (word end or newline).
    /// Tokens include trailing whitespace to mimic LLM output patterns.
    /// </summary>
    private int FindTokenEnd(int start)
    {
        if (start >= _content.Length)
            return _content.Length;

        int pos = start;

        // If we're at a newline, emit it as a standalone token.
        if (_content[pos] == '\n')
            return pos + 1;

        if (_content[pos] == '\r')
        {
            pos++;
            if (pos < _content.Length && _content[pos] == '\n')
                pos++;
            return pos;
        }

        // Walk through non-whitespace characters.
        while (pos < _content.Length && _content[pos] != ' ' && _content[pos] != '\n' && _content[pos] != '\r')
        {
            pos++;
        }

        // Include trailing space as part of the token.
        if (pos < _content.Length && _content[pos] == ' ')
        {
            pos++;
        }

        return pos;
    }

    /// <summary>
    /// Sample markdown content that mimics a coding agent response:
    /// analysis, code changes, file references, diffs, and verification steps.
    /// </summary>
    public static string SampleContent => """
        I found the issue. The `NullReferenceException` on line 42 of `UserService.cs` occurs because `GetUserAsync` can return `null` when the user ID doesn't exist in the database, but the calling code doesn't account for that.

        ## Root Cause

        In `OrderController.cs`, the result of `GetUserAsync` is used directly without a null check:

        ```csharp
        public async Task<IActionResult> PlaceOrder(int userId, OrderRequest request)
        {
            var user = await _userService.GetUserAsync(userId);
            var address = user.DefaultAddress; // NullReferenceException here
            // ...
        }
        ```

        The `GetUserAsync` method returns `null` when no matching record is found:

        ```csharp
        public async Task<User?> GetUserAsync(int id)
        {
            return await _context.Users
                .Include(u => u.DefaultAddress)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        ```

        ## Fix

        I've added a null check that returns a `404 Not Found` response when the user doesn't exist, and added the `[ProducesResponseType]` attributes to keep the OpenAPI spec accurate.

        ```csharp
        [HttpPost("orders")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PlaceOrder(int userId, OrderRequest request)
        {
            var user = await _userService.GetUserAsync(userId);
            if (user is null)
                return NotFound($"User with ID {userId} was not found.");

            var address = user.DefaultAddress
                ?? throw new InvalidOperationException("User has no default address configured.");

            var order = new Order
            {
                UserId = user.Id,
                ShippingAddress = address,
                Items = request.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                CreatedAt = DateTime.UtcNow
            };

            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.Id },
                OrderResponse.FromEntity(order));
        }
        ```

        ## Additional Changes

        I also noticed a few related issues and fixed them:

        - **Missing validation**: `OrderRequest.Items` was not checked for empty collections. Added a `[MinLength(1)]` attribute and a guard clause.
        - **Decimal precision**: `UnitPrice` was stored as `float`, which loses precision for currency. Changed to `decimal` across the model and migration.
        - **Index**: Added a composite index on `(UserId, CreatedAt)` to support the order history query.

        The migration diff:

        ```sql
        ALTER TABLE Orders ALTER COLUMN UnitPrice DECIMAL(18,4) NOT NULL;

        CREATE INDEX IX_Orders_UserId_CreatedAt
            ON Orders (UserId, CreatedAt DESC);
        ```

        ## Test Coverage

        I added three tests covering the new behavior:

        | Test | Scenario | Expected |
        |------|----------|----------|
        | `PlaceOrder_UnknownUser_Returns404` | Non-existent user ID | `404 Not Found` |
        | `PlaceOrder_EmptyItems_Returns400` | Empty item list | `400 Bad Request` |
        | `PlaceOrder_ValidRequest_Returns201` | Happy path | `201 Created` with order |

        ```csharp
        [Fact]
        public async Task PlaceOrder_UnknownUser_Returns404()
        {
            // Arrange
            _userServiceMock
                .Setup(s => s.GetUserAsync(999))
                .ReturnsAsync((User?)null);

            var request = new OrderRequest
            {
                Items = [new OrderItemRequest { ProductId = 1, Quantity = 2, UnitPrice = 9.99m }]
            };

            // Act
            var result = await _controller.PlaceOrder(999, request);

            // Assert
            var notFound = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Contains("999", notFound.Value?.ToString());
        }
        ```

        All 47 tests pass after the changes:

        ```
        dotnet test --filter "OrderController"
        Passed!  - Failed: 0, Passed: 47, Skipped: 0, Total: 47
        ```

        > **Note**: The `decimal` migration is backward-compatible for reads but you will need to run `dotnet ef database update` before deploying. Existing `float` values will be implicitly converted by SQL Server without data loss for values that fit in `DECIMAL(18,4)`.

        Let me know if you want me to split the decimal/index changes into a separate commit.
        """;
}
