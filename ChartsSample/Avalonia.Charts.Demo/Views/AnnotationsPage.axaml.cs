using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class AnnotationsPage : UserControl
{
    public AnnotationsPage()
    {
        InitializeComponent();
        DataContext = new AnnotationsViewModel();
    }
}
