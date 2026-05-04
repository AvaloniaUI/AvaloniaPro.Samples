using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class DataLabelsPage : UserControl
{
    public DataLabelsPage()
    {
        InitializeComponent();
        DataContext = new DataLabelsViewModel();
    }
}
