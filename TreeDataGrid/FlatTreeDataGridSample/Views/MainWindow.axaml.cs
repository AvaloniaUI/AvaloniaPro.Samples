using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using FlatTreeDataGridSample.Models;
using FlatTreeDataGridSample.ViewModels;

namespace FlatTreeDataGridSample.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void countries_CellPrepared(object? sender, TreeDataGridCellEventArgs e)
    {
        if (GetColumnHeader(e.ColumnIndex) != "Population" ||
            DataContext is not MainWindowViewModel viewModel ||
            e.Cell is not TreeDataGridTextCell cell ||
            !countries.TryGetRowModel<Country>(e.Cell, out var country))
        {
            return; 
        }

        // Give Population cells a blue background with an opacity value based on the percent
        // of the maximum population they hold.
        var opacity = (double)country.Population / viewModel.MaxPopulation;
        
        cell.Background = new SolidColorBrush(Colors.Blue, opacity);
    }

    private void countries_CellClearing(object? sender, TreeDataGridCellEventArgs e)
    {
        if (GetColumnHeader(e.ColumnIndex) != "Population" ||
            e.Cell is not TreeDataGridTextCell cell)
        {
            return;
        }

        // Ensure that the population cell background is cleared when it is recycled.
        cell.ClearValue(BackgroundProperty);
    }

    private string? GetColumnHeader(int columnIndex)
    {
        return countries.Columns?[columnIndex].Header as string;
    }
}
