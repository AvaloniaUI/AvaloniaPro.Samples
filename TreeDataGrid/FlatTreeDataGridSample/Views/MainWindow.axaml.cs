using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using FlatTreeDataGridSample.Models;
using FlatTreeDataGridSample.ViewModels;

namespace FlatTreeDataGridSample.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TabControl_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var vm = DataContext as MainWindowViewModel;

        if (vm is not null && tabControl?.SelectedIndex == 1 && vm.Source2.RowSelection?.SelectedIndex != -1)
        {
            // Scroll the selected item into view when switching to the second tab.
            Dispatcher.UIThread.Post(() =>
            {
                countries2.RowsPresenter!.BringIntoView(vm.Source2.RowSelection!.SelectedIndex[0]);
            });
        }
    }
}
