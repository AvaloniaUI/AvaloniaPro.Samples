using Android.App;
using Android.Content.PM;
using Avalonia.Android;

namespace Avalonia.Media.Demo.Android
{
    [Activity(
        Label = "Avalonia.Media.Demo.Android",
        Theme = "@style/MyTheme.NoActionBar",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
    public class MainActivity : AvaloniaMainActivity
    {
    }
}
