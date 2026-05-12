using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Controls.Documents.Primitives.DocumentNodes;
using Avalonia.Controls.Documents.TextModel;
using Avalonia.Controls.Documents.Serialization.Snapshot;
using Avalonia.Controls.Documents.Serialization.Xaml;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Platform;

namespace DocumentViewer.Demo;

public partial class MainWindow : Window
{
    private DocumentSnapshot? _currentSnapshot;


    public MainWindow()
    {
        InitializeComponent();

        // Wire button handlers
        BuildManualButton.Click += OnBuildManualClick;
        BuildFluentButton.Click += OnBuildFluentClick;
        LoadResourceButton.Click += OnLoadResourceClick;
        TakeSnapshotButton.Click += OnTakeSnapshotClick;
        SerializeSnapshotButton.Click += OnSerializeSnapshotClick;
        GenerateLargeDocButton.Click += OnGenerateLargeDocClick;
        GenerateReportButton.Click += OnGenerateReportClick;

        // Handle hyperlink navigation globally
        AddHandler(RichHyperlink.RequestNavigateEvent, OnRequestNavigate);
    }

    // ----------------------------------------------------------------
    // Hyperlink navigation (guide: Styling and Theming > Hyperlinks)
    // ----------------------------------------------------------------

    private void OnRequestNavigate(object? sender, RequestNavigateEventArgs e)
    {
        if (e.Uri is { } uri)
        {
            Process.Start(new ProcessStartInfo(uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }

    // ----------------------------------------------------------------
    // Code-built documents (guide: Building Documents in Code)
    // ----------------------------------------------------------------

    private void OnBuildManualClick(object? sender, RoutedEventArgs e)
    {
        var document = new FlowDocument();

        // Heading
        var heading = new Paragraph
        {
            FontSize = 24,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 0, 0, 10)
        };
        heading.Inlines.Add(new RichRun { Text = "Report Title" });
        document.Blocks.Add(heading);

        // Body paragraph with mixed formatting
        var body = new Paragraph();
        body.Inlines.Add(new RichRun { Text = "Status: " });
        body.Inlines.Add(new RichBold(new RichRun { Text = "Complete" }));
        body.Inlines.Add(new RichRun { Text = ". See " });
        body.Inlines.Add(new RichHyperlink(new RichRun { Text = "details" })
        {
            NavigateUri = new Uri("https://example.com")
        });
        body.Inlines.Add(new RichRun { Text = " for more information." });
        document.Blocks.Add(body);

        // Additional paragraph
        var note = new Paragraph { FontStyle = FontStyle.Italic, Foreground = Brushes.Gray };
        note.Inlines.Add(new RichRun { Text = "This document was built using manual Paragraph/RichRun construction." });
        document.Blocks.Add(note);

        CodeBuiltViewer.Document = document;
    }

    private void OnBuildFluentClick(object? sender, RoutedEventArgs e)
    {
        var document = FlowDocumentBuilder.Create()
            .AddParagraph("Sales Report")
            .AddParagraph()
            .AddText("Body text with ")
            .AddBold("bold")
            .AddText(" and ")
            .AddItalic("italic")
            .AddText(" formatting.")
            .StartList(TextMarkerStyle.Disc)
                .AddListItem("Apples")
                .AddListItem("Bread")
                .AddListItem("Milk")
            .EndList()
            .AddParagraph("Price Table")
            .StartTable()
                .SetTableColumns(new double[] { 200, 100 })
                .StartTableRow()
                    .AddTableCell("Item")
                    .AddTableCell("Price")
                .EndTableRow()
                .StartTableRow()
                    .AddTableCell("Apples")
                    .AddTableCell("$3.00")
                .EndTableRow()
                .StartTableRow()
                    .AddTableCell("Bread")
                    .AddTableCell("$4.50")
                .EndTableRow()
            .EndTable()
            .AddParagraph(
                InlineFactory.Text("Built with "),
                InlineFactory.Bold("FlowDocumentBuilder"),
                InlineFactory.Text(" fluent API."))
            .Build();

        CodeBuiltViewer.Document = document;
    }

    // ----------------------------------------------------------------
    // Embedded resource loading (guide: Loading from Embedded Resources)
    // ----------------------------------------------------------------

    private void OnLoadResourceClick(object? sender, RoutedEventArgs e)
    {
        var uri = new Uri("avares://DocumentViewer.Demo/Assets/Help.xml");
        using var stream = AssetLoader.Open(uri);
        ResourceViewer.Document = FlowDocument.Load(stream, new XamlSerializer());
    }

    // ----------------------------------------------------------------
    // Snapshots (guide: Background Loading and Thread Safety)
    // ----------------------------------------------------------------

    private void OnTakeSnapshotClick(object? sender, RoutedEventArgs e)
    {
        if (SnapshotViewer.Document is null) return;

        _currentSnapshot = SnapshotViewer.Document.CreateSnapshot();
        SerializeSnapshotButton.IsEnabled = true;
        SnapshotOutput.Text = $"Snapshot taken. Generation: {_currentSnapshot.Generation}, " +
                              $"TextLength: {_currentSnapshot.TextLength}";
    }

    private async void OnSerializeSnapshotClick(object? sender, RoutedEventArgs e)
    {
        if (_currentSnapshot is null) return;

        // Serialize the snapshot on a background thread — safe because
        // DocumentSnapshot is immutable and thread-safe.
        using var ms = new MemoryStream();
        await new XamlSerializer().SerializeAsync(_currentSnapshot, ms);
        ms.Position = 0;
        using var reader = new StreamReader(ms);
        SnapshotOutput.Text = await reader.ReadToEndAsync();
    }

    // ----------------------------------------------------------------
    // Virtualization (guide: Performance Considerations)
    // ----------------------------------------------------------------

    private void OnGenerateLargeDocClick(object? sender, RoutedEventArgs e)
    {
        int count = (int)(BlockCountInput.Value ?? 2000);

        var sw = Stopwatch.StartNew();
        var document = new FlowDocument { FontSize = 14 };

        var title = new Paragraph
        {
            FontSize = 22,
            FontWeight = FontWeight.Bold,
            Margin = new Thickness(0, 0, 0, 10)
        };
        title.Inlines.Add(new RichRun { Text = $"Large Document — {count} Paragraphs" });
        document.Blocks.Add(title);

        for (int i = 1; i <= count; i++)
        {
            var p = new Paragraph();
            p.Inlines.Add(new RichRun
            {
                Text = $"Paragraph {i}: The virtualized viewer only realizes blocks within " +
                       "the viewport. Scroll to observe that rendering remains smooth regardless of document size."
            });
            document.Blocks.Add(p);
        }

        sw.Stop();
        VirtualizationViewer.Document = document;
        GenerateTimingText.Text = $"Built {count} blocks in {sw.ElapsedMilliseconds} ms";
    }

    // ----------------------------------------------------------------
    // Dynamic report (guide: Common Patterns > Dynamic Report)
    // ----------------------------------------------------------------

    private void OnGenerateReportClick(object? sender, RoutedEventArgs e)
    {
        var records = new[]
        {
            new SalesRecord("Laptop Pro 15\"", 42, 52499.58m),
            new SalesRecord("Wireless Mouse", 185, 5549.15m),
            new SalesRecord("USB-C Hub", 97, 4849.03m),
            new SalesRecord("Mechanical Keyboard", 63, 6299.37m),
            new SalesRecord("Monitor 27\" 4K", 28, 13999.72m),
            new SalesRecord("Webcam HD", 134, 6699.66m),
            new SalesRecord("Headset Pro", 76, 11399.24m),
        };

        ReportViewer.Document = BuildReport(records);
    }

    private static FlowDocument BuildReport(SalesRecord[] records)
    {
        var builder = FlowDocumentBuilder.Create()
            .AddParagraph("Sales Report");

        builder.StartTable()
            .SetTableColumns(new double[] { 200, 120, 120 })
            .StartTableRow()
                .AddTableCell("Product")
                .AddTableCell("Quantity")
                .AddTableCell("Revenue")
            .EndTableRow();

        foreach (var record in records)
        {
            builder.StartTableRow()
                .AddTableCell(record.Product)
                .AddTableCell(record.Quantity.ToString())
                .AddTableCell(record.Revenue.ToString("C"))
            .EndTableRow();
        }

        builder.EndTable();

        builder.AddParagraph()
            .AddText("Total revenue: ")
            .AddBold(records.Sum(r => r.Revenue).ToString("C"));

        return builder.Build();
    }

    private sealed record SalesRecord(string Product, int Quantity, decimal Revenue);
}
