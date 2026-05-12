using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class HierarchicalChartsPage : UserControl
{
    public HierarchicalChartsPage()
    {
        InitializeComponent();
        DataContext = new HierarchicalChartsViewModel();
    }
}
