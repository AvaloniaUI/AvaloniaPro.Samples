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

    public string PrimaryEnvironmentVariable => "AVALONIA_LICENSE_KEY";

    public string AlternativeEnvironmentVariables =>
        "This is the same license environment variable used by every Avalonia Pro sample.";

    public string LocalOverrideFileName => "Directory.Build.props";

    public string LocalOverrideSnippet =>
        """
        <Project>
          <ItemGroup>
            <AvaloniaUILicenseKey Include="your_key_here" />
          </ItemGroup>
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
