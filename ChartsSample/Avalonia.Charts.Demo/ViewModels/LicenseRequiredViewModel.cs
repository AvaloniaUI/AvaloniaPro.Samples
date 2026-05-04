using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Charts.Demo.ViewModels;

public sealed partial class LicenseRequiredViewModel : ViewModelBase
{
    public LicenseRequiredViewModel(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }

    public string PrimaryEnvironmentVariable => "CHARTS_ACCELERATE_LICENSE_KEY";

    public string AlternativeEnvironmentVariables =>
        "Alternative names also supported: AvAccelerateKey, AccelerateLicenseKey, ACCELERATE_LICENSE_KEY.";

    public string LocalOverrideFileName => "Directory.Build.props.user";

    public string LocalOverrideSnippet =>
        """
        <Project>
          <PropertyGroup>
            <ChartsAccelerateLicenseKey>your_key_here</ChartsAccelerateLicenseKey>
          </PropertyGroup>
        </Project>
        """;

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
