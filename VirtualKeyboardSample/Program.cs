using Avalonia;
using Avalonia.Controls;
using System;
using System.IO;

namespace VirtualKeyboardSample;

class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .WithVirtualKeyboardOptions(new VirtualKeyboardOptions
            {
                DataPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "avalonia-samples", "keyboard", "osk-data")
            })
            .WithVirtualKeyboardRimePlugin()
            .LogToTrace();
}
