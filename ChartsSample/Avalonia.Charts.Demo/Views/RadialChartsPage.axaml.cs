using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class RadialChartsPage : UserControl
{
    public RadialChartsPage()
    {
        InitializeComponent();
        DataContext = new RadialChartsViewModel();
    }
}
