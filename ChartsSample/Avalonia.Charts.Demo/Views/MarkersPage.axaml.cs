using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class MarkersPage : UserControl
{
    public MarkersPage()
    {
        InitializeComponent();
        DataContext = new MarkersViewModel();
    }
}
