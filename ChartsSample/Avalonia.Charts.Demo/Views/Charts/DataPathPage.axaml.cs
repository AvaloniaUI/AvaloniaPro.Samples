using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class DataPathPage : UserControl
{
    public DataPathPage()
    {
        InitializeComponent();
        DataContext = new DataPathViewModel();
    }
}
