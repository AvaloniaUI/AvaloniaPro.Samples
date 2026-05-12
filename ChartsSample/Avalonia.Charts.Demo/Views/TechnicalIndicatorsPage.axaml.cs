using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class TechnicalIndicatorsPage : UserControl
{
    public TechnicalIndicatorsPage()
    {
        InitializeComponent();
        DataContext = new TechnicalIndicatorsViewModel();
    }
}
