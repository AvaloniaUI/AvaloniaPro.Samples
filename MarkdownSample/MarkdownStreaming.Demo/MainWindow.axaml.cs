using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Interactivity;

namespace MarkdownStreaming.Demo;

public partial class MainWindow : Window
{
    private CancellationTokenSource? _cts;
    private MarkdownStreamingSession? _session;
    private int _updateCount;
    private int _totalLength;

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OnStartClick(object? sender, RoutedEventArgs e)
    {
        if (_session != null)
            return;

        btnStart.IsEnabled = false;
        btnStop.IsEnabled = true;
        _updateCount = 0;
        _totalLength = 0;

        txtStatus.Text = "Streaming...";

        _cts = new CancellationTokenSource();
        _session = markdownView.BeginStreaming();
        _session.Updated += OnSessionUpdated;

        var speed = (int)(nudSpeed.Value ?? 60);
        var client = new SimulatedLlmClient(SimulatedLlmClient.SampleContent, speed);

        await foreach (var token in client.StreamAsync(_cts.Token))
        {
            _session.Append(token);
            _totalLength += token.Length;
            txtLength.Text = $"Length: {_totalLength}";
        }

        if (_session != null)
        {
            if (_cts?.IsCancellationRequested == true)
            {
                _session.Complete();
                txtStatus.Text = "Cancelled";
            }
            else
            {
                await _session.CompleteAsync();
                txtStatus.Text = "Completed";
            }
        }

        CleanupSession();
    }

    private void OnStopClick(object? sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
    }

    private void OnResetClick(object? sender, RoutedEventArgs e)
    {
        _cts?.Cancel();
        CleanupSession();

        // Clear the view and reset state

        markdownView.Text = string.Empty;

        _totalLength = 0;
        _updateCount = 0;

        txtStatus.Text = "Ready";
        txtLength.Text = "Length: 0";
        txtUpdates.Text = "Updates: 0";
    }

    private void OnSessionUpdated(object? sender, EventArgs e)
    {
        _updateCount++;
        txtUpdates.Text = $"Updates: {_updateCount}";
   }

    private void CleanupSession()
    {
        if (_session != null)
        {
            _session.Updated -= OnSessionUpdated;
            _session.Dispose();
            _session = null;
        }

        _cts?.Dispose();
        _cts = null;

        btnStart.IsEnabled = true;
        btnStop.IsEnabled = false;
    }
}
