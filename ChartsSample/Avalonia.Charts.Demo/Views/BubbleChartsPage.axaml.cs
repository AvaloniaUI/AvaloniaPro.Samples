using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class BubbleChartsPage : UserControl
{
    public BubbleChartsPage()
    {
        InitializeComponent();
        DataContext = new BubbleChartsViewModel();
    }
}
