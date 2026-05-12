using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels;

namespace Avalonia.Charts.Demo.Views;

public partial class DiagnosticsPage : UserControl
{
    public DiagnosticsPage()
    {
        InitializeComponent();
        DataContext = new DiagnosticsViewModel();
    }
}
