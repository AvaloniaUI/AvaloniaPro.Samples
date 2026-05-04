using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class ContinuousXAxisPage : UserControl
{
    public ContinuousXAxisPage()
    {
        InitializeComponent();
        DataContext = new ContinuousXAxisViewModel();
    }
}
