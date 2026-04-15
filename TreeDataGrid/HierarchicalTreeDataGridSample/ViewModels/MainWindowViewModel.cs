using System.Collections.ObjectModel;
using Avalonia.Controls;
using HierarchicalTreeDataGridSample.Models;

namespace HierarchicalTreeDataGridSample.ViewModels;

/// <summary>
/// Represents the view model for the main window, providing a hierarchical data source
/// for a TreeDataGrid that displays a taxonomic classification system.
/// </summary>
public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ObservableCollection<TaxonomyItem> _data = new(TaxonomyData.GetSampleTaxonomy());

    public MainWindowViewModel()
    {
        // Create a hierarchical TreeDataGrid source
        Source = new HierarchicalTreeDataGridSource<TaxonomyItem>(_data)
            
            // Define an expander column with an inner text column for the scientific name
            .WithHierarchicalExpanderTextColumn(
                "Scientific Name",
                x => x.ScientificName,
                x => x.Children,
                options: o => o.Width = new GridLength(2, GridUnitType.Star))

            // Define a text column for the taxonomic rank
            .WithTextColumn(
                "Taxonomic Rank",
                x => x.TaxonomicRank,
                o => o.Width = new GridLength(1, GridUnitType.Star))

            // Define a column for the common name
            .WithTextColumn(
                "Common Name",
                x => x.CommonName,
                o => o.Width = new GridLength(2, GridUnitType.Star))

            // Define a column for the description
            .WithTextColumn(
                "Description",
                x => x.Description,
                o => o.Width = new GridLength(3, GridUnitType.Star))

            // Define a column for the habitat
            .WithTextColumn(
                x => x.Habitat,
                o => o.Width = new GridLength(2, GridUnitType.Star))

            // Define a column for the conservation status
            .WithTemplateColumnFromResourceKeys(
                "Conservation Status",
                "ConservationStatusTemplate",
                options: o => o.Width = new GridLength(1, GridUnitType.Star));

        // Auto-expand the top level items
        for (var i = 0; i < _data.Count; ++i)
        {
            Source.Expand(new IndexPath(i));
        }
    }

    /// <summary>
    /// Gets the hierarchical data source for the tree data grid.
    /// </summary>
    public HierarchicalTreeDataGridSource<TaxonomyItem> Source { get; }
}
