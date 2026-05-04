using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.Controls.Charts;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class BenchmarkChartsPage : UserControl
{
    private BenchmarkChartsViewModel _viewModel;
    private bool _hasLoaded;

    public BenchmarkChartsPage()
    {
        InitializeComponent();
        _viewModel = new BenchmarkChartsViewModel();
        DataContext = _viewModel;
        Loaded += OnLoaded;
    }

    private async void OnLoaded(object? sender, RoutedEventArgs e)
    {
        if (_hasLoaded)
        {
            return;
        }

        _hasLoaded = true;
        await LoadChartsAsync();
    }

    private async Task LoadChartsAsync()
    {
        await Task.Delay(100);
        
        await RunBenchmark("Chart10K", 10_000, 
            (s) => _viewModel.Status10K = s, 
            (d) => _viewModel.Data10K = d);
            
        await Task.Delay(100);
        
        await RunBenchmark("Chart100K", 100_000, 
            (s) => _viewModel.Status100K = s, 
            (d) => _viewModel.Data100K = d);
            
        await Task.Delay(200);
        
        await RunBenchmark("Chart1M", 1_000_000, 
            (s) => _viewModel.Status1M = s, 
            (d) => _viewModel.Data1M = d);
    }

    private async Task RunBenchmark(string chartName, int pointCount, Action<string> updateStatus, Action<IReadOnlyList<double>> updateData)
    {
        var chart = this.FindControl<CartesianChart>(chartName);
        if (chart == null) return;

        updateStatus("(Generating data...)");
        await Task.Delay(50);
        
        var (data, genTimeMs) = await _viewModel.GenerateDataAsync(pointCount);
        
        updateStatus($"(Gen: {genTimeMs:F0}ms | Rendering...)");
        await Task.Delay(50);
        
        var renderSw = Stopwatch.StartNew();

        updateData(data);

        chart.InvalidateVisual();
        
        // Wait for render pass
        for (int i = 0; i < 3; i++)
        {
            await Task.Delay(1);
            await Dispatcher.UIThread.InvokeAsync(() => { }, DispatcherPriority.Render);
        }
        
        renderSw.Stop();
        var renderTimeMs = renderSw.Elapsed.TotalMilliseconds;
        
        updateStatus($"(Gen: {genTimeMs:F0}ms | Render: {renderTimeMs:F0}ms)");
    }
}
