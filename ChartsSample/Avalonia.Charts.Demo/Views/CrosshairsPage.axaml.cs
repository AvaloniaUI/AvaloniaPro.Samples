using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class CrosshairsPage : UserControl
{
    public CrosshairsPage()
    {
        InitializeComponent();
        DataContext = new CrosshairsViewModel();
    }
}
