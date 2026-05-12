using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class TooltipsPage : UserControl
{
    public TooltipsPage()
    {
        InitializeComponent();
        DataContext = new TooltipsViewModel();
    }
}
