using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class DualAxisPage : UserControl
{
    public DualAxisPage()
    {
        InitializeComponent();
        DataContext = new DualAxisViewModel();
    }
}
