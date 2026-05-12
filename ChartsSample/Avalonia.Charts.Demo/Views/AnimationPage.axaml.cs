using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class AnimationPage : UserControl
{
    public AnimationPage()
    {
        InitializeComponent();
        DataContext = new ViewModels.Charts.AnimationViewModel();
    }

    private void OnReplayLineAnimation(object sender, RoutedEventArgs e) => LineChartControl.ReplayAnimation();
    private void OnReplayAreaAnimation(object sender, RoutedEventArgs e) => AreaChartControl.ReplayAnimation();
    private void OnReplayHistogramAnimation(object sender, RoutedEventArgs e) => HistogramChartControl.ReplayAnimation();
    private void OnReplayBoxPlotAnimation(object sender, RoutedEventArgs e) => BoxPlotChartControl.ReplayAnimation();
    private void OnReplayErrorBarAnimation(object sender, RoutedEventArgs e) => ErrorBarChartControl.ReplayAnimation();
    private void OnReplayPieAnimation(object sender, RoutedEventArgs e) => PieChartControl.ReplayAnimation();
    private void OnReplayDonutAnimation(object sender, RoutedEventArgs e) => DonutChartControl.ReplayAnimation();
    private void OnReplayRadarAnimation(object sender, RoutedEventArgs e) => RadarChartControl.ReplayAnimation();
    private void OnReplayTreeMapAnimation(object sender, RoutedEventArgs e) => TreeMapChartControl.ReplayAnimation();
    private void OnReplayOrgAnimation(object sender, RoutedEventArgs e) => ReplayAnimation("OrgChartControl");
    private void OnReplayArcDiagramAnimation(object sender, RoutedEventArgs e) => ReplayAnimation("ArcDiagramChartControl");
    private void OnReplayLiquidFillAnimation(object sender, RoutedEventArgs e) => LiquidFillGaugeControl.ReplayAnimation();

    private void ReplayAnimation(string controlName)
    {
        if (this.FindControl<Control>(controlName) is not { } control)
        {
            return;
        }

        control.GetType()
            .GetMethod(nameof(ReplayAnimation), Type.EmptyTypes)?
            .Invoke(control, null);
    }
}
