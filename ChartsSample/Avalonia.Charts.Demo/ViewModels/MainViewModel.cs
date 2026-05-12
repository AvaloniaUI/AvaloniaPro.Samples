using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Charts.Demo.ViewModels;

public sealed partial class MainViewModel : ViewModelBase
{
    public ChartsNavigationViewModel Navigation { get; } = new();

    public MainViewModel()
    {
    }

    [RelayCommand]
    private void ToggleTheme()
    {
        var app = Application.Current;
        if (app is null)
        {
            return;
        }

        app.RequestedThemeVariant =
            app.ActualThemeVariant == ThemeVariant.Dark
                ? ThemeVariant.Light
                : ThemeVariant.Dark;
    }
}
