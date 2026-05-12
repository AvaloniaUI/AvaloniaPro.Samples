using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class EngineeringChartsPage : UserControl
{
    public EngineeringChartsPage()
    {
        InitializeComponent();
        DataContext = new EngineeringChartsViewModel();
    }
}
