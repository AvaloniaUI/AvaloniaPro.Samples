using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class TrendlinesPage : UserControl
{
    public TrendlinesPage()
    {
        InitializeComponent();
        DataContext = new TrendlinesViewModel();
    }
}
