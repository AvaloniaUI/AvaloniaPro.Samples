using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class PlotBandsPage : UserControl
{
    public PlotBandsPage()
    {
        InitializeComponent();
        DataContext = new PlotBandsViewModel();
    }
}
