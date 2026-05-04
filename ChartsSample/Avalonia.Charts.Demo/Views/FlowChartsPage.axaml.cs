using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class FlowChartsPage : UserControl
{
    public FlowChartsPage()
    {
        InitializeComponent();
        DataContext = new FlowChartsViewModel();
    }
}

