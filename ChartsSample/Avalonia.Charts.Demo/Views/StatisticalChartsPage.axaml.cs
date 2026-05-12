using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class StatisticalChartsPage : UserControl
{
    public StatisticalChartsPage()
    {
        InitializeComponent();
        DataContext = new StatisticalChartsViewModel();
    }
}
