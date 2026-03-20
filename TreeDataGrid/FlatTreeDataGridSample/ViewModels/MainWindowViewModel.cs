using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using FlatTreeDataGridSample.Models;

namespace FlatTreeDataGridSample.ViewModels;

/// <summary>
/// Represents the view model for the main window, providing a data source for a flat TreeDataGrid
/// that displays a collection of <see cref="Country"/> objects.
/// </summary>
/// <remarks>
/// The data grid source includes predefined columns for displaying country information such as
/// name, region, population, area, and GDP. The columns support features like text search and
/// custom templates  for specific fields.
/// </remarks>
internal partial class MainWindowViewModel : ViewModelBase
{
    private readonly ObservableCollection<Country> _data = new(Countries.All);

    public MainWindowViewModel()
    {
        // Create a flat TreeDataGrid source and define the columns to show.
        Source = new FlatTreeDataGridSource<Country>(_data)
            .WithTextColumn("Country", x => x.Name, o => o.Width = new GridLength(6, GridUnitType.Star))
            
            // Define a template column for the region with custom cell and edit templates.
            .WithTemplateColumnFromResourceKeys("Region", "RegionCell", "RegionEditCell")

            // Define read-only text columns for population and area. The header can be inferred
            // from the property name, so we don't need to specify it explicitly.
            .WithTextColumn(x => x.Population, o =>o.Width = new GridLength(3, GridUnitType.Star))
            .WithTextColumn(x => x.Area, o => o.Width = new GridLength(3, GridUnitType.Star))

            // Define a read-only text column for GDP with right-aligned text and a maximum width.
            .WithTextColumn(x => x.Gdp, o =>
            {
                o.Width = new GridLength(3, GridUnitType.Star);
                o.TextAlignment = Avalonia.Media.TextAlignment.Right;
                o.MaxWidth = new GridLength(150);
            });

        MaxPopulation = _data.Max(x => x.Population);
    }

    /// <summary>
    /// Gets the data source for the flat tree data grid, containing a collection of
    /// <see cref="Country"/> objects.
    /// </summary>
    public FlatTreeDataGridSource<Country> Source { get; }

    /// <summary>
    /// Gets the maximum population of all the countries.
    /// </summary>
    public int MaxPopulation { get; }
}
