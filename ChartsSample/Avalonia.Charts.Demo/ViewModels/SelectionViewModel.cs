using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class SelectionItem
{
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public List<SelectionItem> Children { get; set; } = new();
}

public partial class SelectionViewModel : ViewModelBase
{
    public SelectionViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        SingleSelectionData = new ObservableCollection<int> { 45, 72, 38, 65, 50 };
        SingleDeselectData = new ObservableCollection<int> { 120, 150, 180, 140, 200, 175 };
        MultiSelectionData = new ObservableCollection<int> { 85, 120, 65, 95, 110, 75, 130 };
        EventsData = new ObservableCollection<int> { 50, 80, 40, 90, 60 };
        ContinuousSelectionData = new ObservableCollection<ContinuousDatePoint>
        {
            new(new DateTime(2026, 4, 1), 30),
            new(new DateTime(2026, 4, 2), 42),
            new(new DateTime(2026, 4, 5), 36),
            new(new DateTime(2026, 4, 11), 58),
            new(new DateTime(2026, 4, 18), 52)
        };
        
        SingleSelectionStatus = "Click a bar to select.";
        SingleDeselectStatus = "Click to select, click again to deselect.";
        MultiSelectionStatus = "Click bars to select multiple.";
        ContinuousSelectionStatus = "Click a marker to select a DateTime point.";
        EventLog = "Click a bar to see events fire.";

        PieSelectionData = new ObservableCollection<int> { 30, 20, 10, 40 };
        PieSelectionStatus = "Click a slice to select.";
        
        SunburstSelectionData = new List<SelectionItem>
        {
            new SelectionItem 
            { 
                Name = "Root 1", 
                Value = 100,
                Children = new List<SelectionItem>
                {
                    new SelectionItem { Name = "Child 1.1", Value = 40 },
                    new SelectionItem { Name = "Child 1.2", Value = 60 }
                }
            },
            new SelectionItem 
            { 
                Name = "Root 2", 
                Value = 80,
                Children = new List<SelectionItem>
                {
                    new SelectionItem { Name = "Child 2.1", Value = 30 },
                    new SelectionItem { Name = "Child 2.2", Value = 50 }
                }
            }
        };
        SunburstSelectionStatus = "Click a segment to select.";

        TreeMapSelectionData = new List<SelectionItem>
        {
            new SelectionItem { Name = "A", Value = 10 },
            new SelectionItem { Name = "B", Value = 20 },
            new SelectionItem { Name = "C", Value = 30 },
            new SelectionItem { Name = "D", Value = 40 },
        };
        TreeMapSelectionStatus = "Click a rectangle to select.";

        FunnelSelectionData = new List<SelectionItem>
        {
            new SelectionItem { Name = "Step 1", Value = 100 },
            new SelectionItem { Name = "Step 2", Value = 80 },
            new SelectionItem { Name = "Step 3", Value = 60 },
            new SelectionItem { Name = "Step 4", Value = 40 },
        };
        FunnelSelectionStatus = "Click a trapezoid to select.";

        RadarSelectionData = new List<SelectionItem>
        {
            new SelectionItem { Name = "Metric A", Value = 80 },
            new SelectionItem { Name = "Metric B", Value = 65 },
            new SelectionItem { Name = "Metric C", Value = 90 },
            new SelectionItem { Name = "Metric D", Value = 55 },
            new SelectionItem { Name = "Metric E", Value = 70 },
        };
        RadarSelectionStatus = "Click a vertex/marker to select.";

        VennSelectionData = new ObservableCollection<VennItem>
        {
            new VennItem { Sets = new[] { "A" }, Value = 10, Name = "Set A" },
            new VennItem { Sets = new[] { "B" }, Value = 15, Name = "Set B" },
            new VennItem { Sets = new[] { "A", "B" }, Value = 5, Name = "Intersection" }
        };
        VennSelectionStatus = "Click a circle or intersection to select.";

        FinancialSelectionData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(24));
        PointAndFigureSelectionData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(80));

    }

    private static IEnumerable<FinancialPoint> GenerateFinancialData(int count)
    {
        var random = new Random(71);
        var price = 72.0;
        var date = DateTime.Today.AddDays(-count);

        for (var i = 0; i < count; i++)
        {
            var open = price;
            var close = open + (random.NextDouble() - 0.45) * 4.0;
            var high = Math.Max(open, close) + random.NextDouble() * 1.8;
            var low = Math.Min(open, close) - random.NextDouble() * 1.8;
            var brush = close >= open
                ? new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#0EA5E9"))
                : new Avalonia.Media.SolidColorBrush(Avalonia.Media.Color.Parse("#F97316"));

            yield return new FinancialPoint(date.AddDays(i), open, high, low, close) { Brush = brush };
            price = close;
        }
    }

    [ObservableProperty] private ObservableCollection<int> _singleSelectionData = null!;
    [ObservableProperty] private ObservableCollection<int> _singleDeselectData = null!;
    [ObservableProperty] private ObservableCollection<int> _multiSelectionData = null!;
    [ObservableProperty] private ObservableCollection<int> _eventsData = null!;
    [ObservableProperty] private ObservableCollection<ContinuousDatePoint> _continuousSelectionData = null!;

    [ObservableProperty] private string _singleSelectionStatus = null!;
    [ObservableProperty] private string _singleDeselectStatus = null!;
    [ObservableProperty] private string _multiSelectionStatus = null!;
    [ObservableProperty] private string _continuousSelectionStatus = null!;
    [ObservableProperty] private string _eventLog = null!;

    [ObservableProperty] private ObservableCollection<int> _pieSelectionData = null!;
    [ObservableProperty] private string _pieSelectionStatus = null!;

    [ObservableProperty] private List<SelectionItem> _sunburstSelectionData = null!;
    [ObservableProperty] private string _sunburstSelectionStatus = null!;

    [ObservableProperty] private List<SelectionItem> _treeMapSelectionData = null!;

    [ObservableProperty] private string _treeMapSelectionStatus = null!;

    [ObservableProperty] private List<SelectionItem> _funnelSelectionData = null!;
    [ObservableProperty] private string _funnelSelectionStatus = null!;

    [ObservableProperty] private List<SelectionItem> _radarSelectionData = null!;
    [ObservableProperty] private string _radarSelectionStatus = null!;

    // Methods invoked by the view to translate selection events into status text updates.
    
    public void UpdateSingleSelectionStatus(int index)
    {
        SingleSelectionStatus = index >= 0 ? $"Selected: Bar {index + 1}" : "No selection";
    }

    public void UpdateSingleDeselectStatus(int index)
    {
        SingleDeselectStatus = index >= 0 ? $"Selected: Bar {index + 1}" : "No selection (deselected)";
    }

    public void UpdateMultiSelectionStatus(System.Collections.Generic.List<int> indexes)
    {
        MultiSelectionStatus = indexes.Count > 0 
            ? $"Selected: Bars {string.Join(", ", indexes.Select(i => i + 1))}" 
            : "No selection";
    }

    public void UpdateContinuousSelectionStatus(int index)
    {
        if (index < 0 || index >= ContinuousSelectionData.Count)
        {
            ContinuousSelectionStatus = "No DateTime point selected.";
            return;
        }

        var point = ContinuousSelectionData[index];
        ContinuousSelectionStatus = $"Selected: {point.Date:MMM dd} = {point.Value:N0}";
    }
    
    public void UpdateEventLog(string message)
    {
        EventLog = message;
    }
    
    public void AppendToEventLog(string message)
    {
        EventLog += "\n" + message;
    }

    public void UpdatePieSelectionStatus(int index)
    {
        PieSelectionStatus = index >= 0 ? $"Selected Slice Index: {index}" : "No slice selected";
    }

    public void UpdateSunburstSelectionStatus(int index)
    {
        SunburstSelectionStatus = index >= 0 ? $"Selected Segment Index: {index}" : "No segment selected";
    }
    
    public void UpdateTreeMapSelectionStatus(int index)
    {
        TreeMapSelectionStatus = index >= 0 ? $"Selected Node Index: {index}" : "No node selected";
    }

    public void UpdateFunnelSelectionStatus(int index)
    {
        FunnelSelectionStatus = index >= 0 ? $"Selected Step Index: {index}" : "No step selected";
    }

    public void UpdateRadarSelectionStatus(int index)
    {
        RadarSelectionStatus = index >= 0 ? $"Selected Vertex Index: {index}" : "No vertex selected";
    }

    [ObservableProperty] private ObservableCollection<VennItem> _vennSelectionData = null!;
    [ObservableProperty] private string _vennSelectionStatus = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _financialSelectionData = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _pointAndFigureSelectionData = null!;

    public void UpdateVennSelectionStatus(int index)
    {
        VennSelectionStatus = index >= 0 ? $"Selected Venn Item Index: {index}" : "No item selected";
    }
}
