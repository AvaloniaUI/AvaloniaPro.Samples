using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Platform;

namespace Avalonia.Charts.Demo.Views;

public partial class MainView : UserControl
{
    private IInsetsManager? _insetsManager;

    public MainView()
    {
        InitializeComponent();

        TopLevel.SetAutoSafeAreaPadding(this, false);

        AttachedToVisualTree += OnAttachedToVisualTree;
        DetachedFromVisualTree += OnDetachedFromVisualTree;
    }

    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        _insetsManager = TopLevel.GetTopLevel(this)?.InsetsManager;
        if (_insetsManager is not null)
        {
            _insetsManager.SafeAreaChanged += OnSafeAreaChanged;
        }

        UpdateToolbarPadding();
    }

    private void OnDetachedFromVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (_insetsManager is not null)
        {
            _insetsManager.SafeAreaChanged -= OnSafeAreaChanged;
            _insetsManager = null;
        }
    }

    private void OnSafeAreaChanged(object? sender, SafeAreaChangedArgs e)
    {
        UpdateToolbarPadding();
    }

    private void UpdateToolbarPadding()
    {
        var safeAreaTop = _insetsManager?.SafeAreaPadding.Top ?? 0;
        ToolbarBorder.Padding = safeAreaTop > 0
            ? new Thickness(12, safeAreaTop + 4, 12, 6)
            : new Thickness(12);
    }
}
