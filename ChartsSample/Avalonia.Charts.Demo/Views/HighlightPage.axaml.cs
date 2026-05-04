using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class HighlightPage : UserControl
{
    public HighlightPage()
    {
        InitializeComponent();
        DataContext = new HighlightViewModel();
    }
}
