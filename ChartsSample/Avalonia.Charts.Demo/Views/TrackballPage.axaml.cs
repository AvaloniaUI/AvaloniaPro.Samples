using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class TrackballPage : UserControl
{
    public TrackballPage()
    {
        InitializeComponent();
        DataContext = new TrackballViewModel();
    }
}
