using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Charts;
using Avalonia.Charts.Demo.ViewModels.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class SelectionPage : UserControl
{
    private SelectionViewModel _viewModel;

    public SelectionPage()
    {
        InitializeComponent();
        _viewModel = new SelectionViewModel();
        DataContext = _viewModel;

        InitializeEventHandlers();
    }

    private void InitializeEventHandlers()
    {
        // Single Selection
        if (SingleSelectionChart.Series.FirstOrDefault() is BarSeries singleSeries)
        {
            singleSeries.SelectionChanged += (_, e) =>
            {
                _viewModel.UpdateSingleSelectionStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
            };
        }

        // Single Deselect
        if (SingleDeselectChart.Series.FirstOrDefault() is BarSeries deselectSeries)
        {
            deselectSeries.SelectionChanged += (_, e) =>
            {
                _viewModel.UpdateSingleDeselectStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
            };
        }

        // Multiple Selection
        if (MultipleSelectionChart.Series.FirstOrDefault() is BarSeries multiSeries)
        {
            multiSeries.SelectionChanged += (_, _) =>
            {
                _viewModel.UpdateMultiSelectionStatus(multiSeries.SelectedIndexes.ToList());
            };
            
            ClearSelectionBtn.Click += (_, _) =>
            {
                multiSeries.ClearSelection();
                _viewModel.UpdateMultiSelectionStatus(new System.Collections.Generic.List<int>());
            };
        }

        if (ContinuousSelectionChart.Series.FirstOrDefault() is LineSeries continuousSeries)
        {
            continuousSeries.SelectionChanged += (_, e) =>
            {
                _viewModel.UpdateContinuousSelectionStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
            };
        }

        // Events
        if (EventsChart.Series.FirstOrDefault() is BarSeries eventsSeries)
        {
            eventsSeries.SelectionChanging += (_, e) =>
            {
                _viewModel.UpdateEventLog($"SelectionChanging: New=[{string.Join(",", e.NewIndexes.Cast<object>())}], Old=[{string.Join(",", e.OldIndexes.Cast<object>())}]");
            };
            eventsSeries.SelectionChanged += (_, e) =>
            {
                _viewModel.AppendToEventLog($"SelectionChanged: New=[{string.Join(",", e.NewIndexes.Cast<object>())}], Old=[{string.Join(",", e.OldIndexes.Cast<object>())}]");
            };
        }
        
        // Pie Selection
        if (PieSelectionChart.Series.FirstOrDefault() is PieSeries pieSeries)
        {
            pieSeries.SelectionChanged += (_, e) =>
            {
                _viewModel.UpdatePieSelectionStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
            };
        }

        // Sunburst Selection
        SunburstSelectionChart.SelectionChanged += (_, e) =>
        {
            _viewModel.UpdateSunburstSelectionStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
        };

        // TreeMap Selection
        TreeMapSelectionChart.SelectionChanged += (_, e) =>
        {
            _viewModel.UpdateTreeMapSelectionStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
        };

        // Funnel Selection
        FunnelSelectionChart.SelectionChanged += (_, e) =>
        {
            _viewModel.UpdateFunnelSelectionStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
        };

        // Radar Selection
        if (RadarSelectionChart.Series.FirstOrDefault() is RadarSeries radarSeries)
        {
            radarSeries.SelectionChanged += (_, e) =>
            {
                _viewModel.UpdateRadarSelectionStatus(e.NewIndexes.Count > 0 && e.NewIndexes[0] is int i ? i : -1);
            };
        }
    }

}
