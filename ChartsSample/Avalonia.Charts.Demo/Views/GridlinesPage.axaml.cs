using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class GridlinesPage : UserControl
{
    public GridlinesPage()
    {
        InitializeComponent();
        DataContext = new GridlinesViewModel();
    }
}
