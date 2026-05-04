using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class ItemCustomizationPage : Avalonia.Controls.UserControl
{
    public ItemCustomizationPage()
    {
        InitializeComponent();
        DataContext = new ItemCustomizationViewModel();
    }
}
