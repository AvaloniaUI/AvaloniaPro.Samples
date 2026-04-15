using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NativeWebViewAndDialogSample.ViewModels;

public partial class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        Url = "https://docs.avaloniaui.net/controls/web/nativewebview";
        NavigationUrl = new Uri(Url);
    }

    [ObservableProperty]
    public partial string Url { get; set; }

    [ObservableProperty]
    public partial Uri NavigationUrl { get; set; }

    public ObservableCollection<string> Log { get; } = [];

    [RelayCommand]
    private void Navigate()
    {
        try
        {
            NavigationUrl = new Uri(Url);
        }
        catch (Exception ex)
        {
            Log.Add(ex.Message);
        }
    }

    [RelayCommand]
    private void NavigateDialog()
    {
        try
        {
            var url = new Uri(Url);
            var dialog = new NativeWebDialog
            {
                Source = url
            };
            dialog.Resize(400, 500);
            dialog.Show();
        }
        catch (Exception ex)
        {
            Log.Add(ex.Message);
        }
    }
}
