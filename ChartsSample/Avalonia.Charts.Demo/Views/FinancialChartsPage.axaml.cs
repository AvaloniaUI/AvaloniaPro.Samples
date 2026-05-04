using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class FinancialChartsPage : UserControl
{
    public FinancialChartsPage()
    {
        InitializeComponent();
        DataContext = new FinancialChartsViewModel();
    }
}
