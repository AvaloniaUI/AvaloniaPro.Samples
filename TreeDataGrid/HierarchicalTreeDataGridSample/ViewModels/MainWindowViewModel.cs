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
        {
            Columns =
            {
                // Define an expander column with an inner text column for the scientific name
                TreeDataGridHierarchicalExpanderColumn.Create<TaxonomyItem>(
                    "Scientific Name",
                    TreeDataGridTextColumn.Create<TaxonomyItem, string>(
                        null,
                        x => x.ScientificName),
                    x => x.Children,
                    width: new GridLength(2, GridUnitType.Star)),

                // Define a text column for the taxonomic rank
                TreeDataGridTextColumn.Create<TaxonomyItem, string>(
                    "Taxonomic Rank",
                    x => x.TaxonomicRank,
                    width: new GridLength(1, GridUnitType.Star)),

                // Define a column for the common name
                TreeDataGridTextColumn.Create<TaxonomyItem, string>(
                    "Common Name",
                    x => x.CommonName,
                    width: new GridLength(2, GridUnitType.Star)),

                // Define a column for the description
                TreeDataGridTextColumn.Create<TaxonomyItem, string>(
                    "Description",
                    x => x.Description,
                    width: new GridLength(3, GridUnitType.Star)),

                // Define a column for the habitat
                TreeDataGridTextColumn.Create<TaxonomyItem, string>(
                    "Habitat",
                    x => x.Habitat,
                    width: new GridLength(2, GridUnitType.Star)),

                // Define a column for the conservation status
                TreeDataGridTemplateColumn.CreateFromResourceKeys(
                    "Conservation Status",
                    "ConservationStatusTemplate",
                    width: new GridLength(1, GridUnitType.Star))
            }
        };

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
