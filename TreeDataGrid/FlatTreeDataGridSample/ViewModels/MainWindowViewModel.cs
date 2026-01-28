using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
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
        Source1 = new FlatTreeDataGridSource<Country>(_data)
        {
            Columns =
            {
                // Define a read/write text column for the country name with text search enabled.
                new TextColumn<Country, string>(
                    "Country",
                    x => x.Name,
                    (o, v) => o.Name = v, 
                    new GridLength(6, GridUnitType.Star), new()
                {
                    IsTextSearchEnabled = true,
                }),

                // Define read-only text columns for population and area.
                new TextColumn<Country, int>("Population", x => x.Population, new GridLength(3, GridUnitType.Star)),
                new TextColumn<Country, int>("Area", x => x.Area, new GridLength(3, GridUnitType.Star)),

                // Define a read-only text column for GDP with right-aligned text and a maximum width.
                new TextColumn<Country, int>("GDP", x => x.Gdp, new GridLength(3, GridUnitType.Star), new()
                {
                    TextAlignment = Avalonia.Media.TextAlignment.Right,
                    MaxWidth = new GridLength(150)
                }),
            }
        };

        Source1.RowSelection!.SelectionChanged += (s, e) =>
        {
            Source2!.RowSelection!.SelectedIndex = Source1.RowSelection!.SelectedIndex;
        };

        Source2 = new FlatTreeDataGridSource<Country>(_data)
        {
            Columns =
            {
                // Define a read/write text column for the country name with text search enabled.
                new TextColumn<Country, string>(
                    "Country",
                    x => x.Name,
                    (o, v) => o.Name = v,
                    new GridLength(6, GridUnitType.Star), new()
                {
                    IsTextSearchEnabled = true,
                }),

                // Define read-only text columns for population and area.
                new TextColumn<Country, int>("Population", x => x.Population, new GridLength(3, GridUnitType.Star)),
                new TextColumn<Country, int>("Area", x => x.Area, new GridLength(3, GridUnitType.Star)),

                // Define a read-only text column for GDP with right-aligned text and a maximum width.
                new TextColumn<Country, int>("GDP", x => x.Gdp, new GridLength(3, GridUnitType.Star), new()
                {
                    TextAlignment = Avalonia.Media.TextAlignment.Right,
                    MaxWidth = new GridLength(150)
                }),
            }
        };

        MaxPopulation = _data.Max(x => x.Population);
    }

    public FlatTreeDataGridSource<Country> Source1 { get; }
    public FlatTreeDataGridSource<Country> Source2 { get; }

    /// <summary>
    /// Gets the maximum population of all the countries.
    /// </summary>
    public int MaxPopulation { get; }
}
