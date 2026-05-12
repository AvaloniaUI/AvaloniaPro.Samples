using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class AppearanceChartsPage : UserControl
{
    public AppearanceChartsPage()
    {
        InitializeComponent();
        DataContext = new AppearanceChartsViewModel();
    }
}
