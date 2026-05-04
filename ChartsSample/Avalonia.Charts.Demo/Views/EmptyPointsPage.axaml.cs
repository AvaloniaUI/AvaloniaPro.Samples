using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class EmptyPointsPage : UserControl
{
    public EmptyPointsPage()
    {
        InitializeComponent();
        DataContext = new EmptyPointsViewModel();
    }
}
