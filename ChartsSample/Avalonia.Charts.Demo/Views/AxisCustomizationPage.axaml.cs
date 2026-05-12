using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class AxisCustomizationPage : UserControl
{
    public AxisCustomizationPage()
    {
        InitializeComponent();
        DataContext = new AxisCustomizationViewModel();
    }
}
