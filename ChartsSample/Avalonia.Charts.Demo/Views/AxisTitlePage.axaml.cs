using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class AxisTitlePage : UserControl
{
    public AxisTitlePage()
    {
        InitializeComponent();
        DataContext = new AxisTitleViewModel();
    }
}
