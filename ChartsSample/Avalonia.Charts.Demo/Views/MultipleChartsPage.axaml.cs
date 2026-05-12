using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class MultipleChartsPage : UserControl
{
    public MultipleChartsPage()
    {
        InitializeComponent();
        DataContext = new MultipleChartsViewModel();
    }
}
