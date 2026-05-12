using Avalonia;
using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class RealTimeChartsPage : UserControl
{
    private RealTimeChartsViewModel? _viewModel;

    public RealTimeChartsPage()
    {
        InitializeComponent();
        _viewModel = new RealTimeChartsViewModel();
        DataContext = _viewModel;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _viewModel?.Resume();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _viewModel?.Pause();
    }
}