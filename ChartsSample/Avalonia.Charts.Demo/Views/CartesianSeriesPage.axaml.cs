using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class CartesianSeriesPage : UserControl
{
    public CartesianSeriesPage()
    {
        InitializeComponent();
        DataContext = new CartesianSeriesViewModel();
    }
}
