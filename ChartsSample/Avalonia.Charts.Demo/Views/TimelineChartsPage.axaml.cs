using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class TimelineChartsPage : UserControl
{
    public TimelineChartsPage()
    {
        InitializeComponent();
        DataContext = new TimelineChartsViewModel();
    }
}
