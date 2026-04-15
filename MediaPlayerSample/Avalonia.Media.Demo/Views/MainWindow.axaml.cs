using System;
using System.Runtime.InteropServices;
using Avalonia.Media.Demo.ViewModels;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Media.Demo.Views;

public partial class MainWindow : Window
{
    private MainViewModel? _mainVm;
    private WindowState _oldState;

    public MainWindow()
    {
        InitializeComponent();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            TransparencyLevelHint = [WindowTransparencyLevel.AcrylicBlur,];
            ExtendClientAreaToDecorationsHint = true;
            WindowDecorations = WindowDecorations.None;
            ExtendClientAreaTitleBarHeightHint = 48;
        }
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        _mainVm = DataContext as MainViewModel;

        if (_mainVm is null) throw new InvalidOperationException("MainViewModel can't be null.");

        _mainVm.FullScreenCommand = new RelayCommand(HandleFullScreen);

        _mainVm.EnableTransparency = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

        _mainVm.DragMoveCommand = new RelayCommand<PointerPressedEventArgs>(HandleDragMove);

        base.OnDataContextChanged(e);
    }

    private void HandleDragMove(PointerPressedEventArgs? e)
    {
        if (e == null) return;
        switch (e.ClickCount)
        {
            case 2 when WindowState is WindowState.Normal:
                WindowState = WindowState.Maximized;
                break;
            case 2:
            {
                if (WindowState is WindowState.Maximized )
                    WindowState = WindowState.Normal;
                break;
            }
            case 1 when WindowState is not WindowState.FullScreen:
                BeginMoveDrag(e);
                break;
        }
    }

    private void HandleFullScreen()
    {
        if (WindowState is WindowState.FullScreen)
        {
            WindowState = _oldState;

            if (_mainVm != null)
                _mainVm.IsFullScreen = false;
        }
        else if (WindowState is not WindowState.FullScreen)
        {
            _oldState = WindowState;
            WindowState = WindowState.FullScreen;

            if (_mainVm != null)
            {
                _mainVm.IsFullScreen = true;
            }
        }
    }
}
