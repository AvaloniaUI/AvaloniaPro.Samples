using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class AnalyticsChartsPage : UserControl
{
    public AnalyticsChartsPage()
    {
        InitializeComponent();
        DataContext = new AnalyticsChartsViewModel();
    }
}
