using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class MapChartsPage : UserControl
{
    public MapChartsPage()
    {
        InitializeComponent();
        DataContext = new MapChartsViewModel();
    }
}
