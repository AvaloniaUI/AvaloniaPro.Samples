using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class SmartLabelsPage : UserControl
{
    public SmartLabelsPage()
    {
        InitializeComponent();
        DataContext = new SmartLabelsViewModel();
    }
}
