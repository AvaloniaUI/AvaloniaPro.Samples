using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaUI.Licensing;

namespace Avalonia.Charts.Demo;

public partial class App : Application
{
    public const string DisplayName = "Avalonia Charts Demo";

    public override void Initialize()
    {
        Name = DisplayName;
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                Title = DisplayName,
                Content = CreateStartupView()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = CreateStartupView();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static Control CreateStartupView()
    {
        try
        {
            return new Views.MainView
            {
                DataContext = new ViewModels.MainViewModel()
            };
        }
        catch (Exception ex) when (TryGetLicensingException(ex, out var licensingException))
        {
            return new Views.LicenseRequiredView
            {
                DataContext = new ViewModels.LicenseRequiredViewModel(licensingException.Message)
            };
        }
    }

    private static bool TryGetLicensingException(Exception ex, out AvaloniaLicensingException licensingException)
    {
        for (var current = ex; current is not null; current = current.InnerException)
        {
            if (current is AvaloniaLicensingException found)
            {
                licensingException = found;
                return true;
            }
        }

        licensingException = null!;
        return false;
    }
}
