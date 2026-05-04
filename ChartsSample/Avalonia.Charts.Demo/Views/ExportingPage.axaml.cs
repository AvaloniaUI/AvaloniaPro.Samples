using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Avalonia.Controls.Charts;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views;

public partial class ExportingPage : UserControl
{
    public ExportingPage()
    {
        InitializeComponent();
        DataContext = new ExportingViewModel();
    }

    private async void OnExportPngClick(object? sender, RoutedEventArgs e)
    {
        if (this.FindControl<CartesianChart>("ExportChart") is { } chart)
        {
            try
            {
                // Use built-in async export which opens a save dialog for PNG
                await chart.ExportAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Export failed: {ex.Message}");
            }
        }
    }

    private async void OnExportJpgClick(object? sender, RoutedEventArgs e)
    {
        if (this.FindControl<CartesianChart>("ExportChart") is { } chart)
        {
            try
            {
                var path = await PickExportPathAsync(
                    "Export to JPG",
                    "chart.jpg",
                    ".jpg",
                    new FilePickerFileType("JPEG Image") { Patterns = new[] { "*.jpg", "*.jpeg" } });

                if (path != null)
                {
                    await chart.ExportAsync(path);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Export failed: {ex.Message}");
            }
        }
    }

    private async System.Threading.Tasks.Task<string?> PickExportPathAsync(
        string title,
        string suggestedFileName,
        string defaultExtension,
        FilePickerFileType fileType)
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
        {
            var path = await PickMacOsExportPathAsync(title, suggestedFileName);
            return path != null ? EnsureExtension(path, defaultExtension) : null;
        }

        var topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null) return null;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            FileTypeChoices = new[] { fileType },
            DefaultExtension = defaultExtension.TrimStart('.'),
            SuggestedFileName = suggestedFileName
        });

        return file?.Path.LocalPath;
    }

    private static async System.Threading.Tasks.Task<string?> PickMacOsExportPathAsync(
        string title,
        string suggestedFileName)
    {
        var script =
            $"POSIX path of (choose file name with prompt \"{EscapeAppleScriptString(title)}\" default name \"{EscapeAppleScriptString(suggestedFileName)}\")";
        var startInfo = new System.Diagnostics.ProcessStartInfo("/usr/bin/osascript")
        {
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };
        startInfo.ArgumentList.Add("-e");
        startInfo.ArgumentList.Add(script);

        using var process = System.Diagnostics.Process.Start(startInfo);
        if (process == null) return null;

        var outputTask = process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        var output = (await outputTask).Trim();
        return process.ExitCode == 0 && !string.IsNullOrWhiteSpace(output) ? output : null;
    }

    private static string EnsureExtension(string path, string extension) =>
        System.IO.Path.HasExtension(path) ? path : path + extension;

    private static string EscapeAppleScriptString(string value) =>
        value.Replace("\\", "\\\\").Replace("\"", "\\\"");
}
