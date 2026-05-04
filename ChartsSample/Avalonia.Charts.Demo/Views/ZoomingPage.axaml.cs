using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Controls.Charts;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class ZoomingPage : UserControl
{
    private CartesianChart? _panChart;

    private TextBlock? _panChartZoomInfo;

    private CartesianChart? _horizontalChart;
    private CartesianChart? _verticalChart;
    private CartesianChart? _barChart;

    private StackPanel? _chartXYEventsContainer;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Avalonia.NameReference", "AVP1000:Unknown named control reference", Justification = "References are correct")]
    public ZoomingPage()
    {
        InitializeComponent();
        DataContext = new ZoomingViewModel();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);

        // Get chart references
        _panChart = this.FindControl<CartesianChart>("ChartXY");
        _horizontalChart = this.FindControl<CartesianChart>("ChartHorizontal");
        _verticalChart = this.FindControl<CartesianChart>("ChartVertical");
        _barChart = this.FindControl<CartesianChart>("ChartBar");

        // Get TextBlock references
        _panChartZoomInfo = this.FindControl<TextBlock>("PanChartZoomInfo");
        _chartXYEventsContainer = this.FindControl<StackPanel>("ChartXYEventsContainer");

        // Subscribe to events
        if (_panChart != null) _panChart.ZoomChanged += OnZoomChanged;
        if (_horizontalChart != null) _horizontalChart.ZoomChanged += OnZoomChanged;
        if (_verticalChart != null) _verticalChart.ZoomChanged += OnZoomChanged;
        if (_barChart != null) _barChart.ZoomChanged += OnZoomChanged;
    }

    // Event handlers for PanChart
    private void OnZoomChanged(object? sender, ChartZoomEventArgs e)
    {
        if (sender is CartesianChart chart)
        {
             // Independent sample logic: specific log for ChartXY
             if (chart == _panChart)
             {
                 if (_panChartZoomInfo != null)
                 {
                     _panChartZoomInfo.Text = $"Zoom: X={_panChart.ZoomFactorX:P0}, Y={_panChart.ZoomFactorY:P0}";
                 }

                 if (_chartXYEventsContainer != null)
                 {
                     var msg = $"[{DateTime.Now:HH:mm:ss}] Zoom: X={chart.ZoomFactorX:P0}, Y={chart.ZoomFactorY:P0}";
                     var tb = new TextBlock 
                     { 
                         Text = msg, 
                         FontSize = 11, 
                         FontFamily = new FontFamily("Monospace") 
                     };
                     _chartXYEventsContainer.Children.Insert(1, tb); // Insert after header
                     
                     // Keep log size manageable
                     if (_chartXYEventsContainer.Children.Count > 10)
                     {
                         _chartXYEventsContainer.Children.RemoveAt(_chartXYEventsContainer.Children.Count - 1);
                     }
                 }
             }
        }
    }

    // Reset and GoBack handlers for PanChart
    private void OnBackClick(object? sender, RoutedEventArgs e)
    {
        _panChart?.GoBackZoom();
    }

    private void OnResetClick(object? sender, RoutedEventArgs e)
    {
        _panChart?.ResetZoom();
        if (_panChartZoomInfo != null)
        {
            _panChartZoomInfo.Text = "Zoom: X=100%, Y=100%";
        }
    }
}
