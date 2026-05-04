using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class GaugeChartsPage : UserControl
{
    public GaugeChartsPage()
    {
        InitializeComponent();
        DataContext = new GaugeChartsViewModel();
    }
}

