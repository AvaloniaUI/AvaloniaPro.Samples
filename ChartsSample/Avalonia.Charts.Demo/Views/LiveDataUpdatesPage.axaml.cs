using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class LiveDataUpdatesPage : UserControl
{
    public LiveDataUpdatesPage()
    {
        InitializeComponent();
        DataContext = new LiveDataUpdatesViewModel();
    }
}
