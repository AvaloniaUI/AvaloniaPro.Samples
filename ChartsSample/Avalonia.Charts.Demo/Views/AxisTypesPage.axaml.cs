using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class AxisTypesPage : UserControl
{
    public AxisTypesPage()
    {
        InitializeComponent();
        DataContext = new AxisTypesViewModel();
    }
}
