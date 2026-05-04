using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class LegendChartsPage : UserControl
{
    public LegendChartsPage()
    {
        InitializeComponent();
        DataContext = new LegendChartsViewModel();
    }
}
