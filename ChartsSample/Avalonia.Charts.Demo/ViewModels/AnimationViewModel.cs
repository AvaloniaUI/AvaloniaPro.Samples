using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Charts.Demo.Models;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class AnimationViewModel : ObservableObject
{
    public ChartSampleViewModel LineChart { get; } = new();
    public ChartSampleViewModel AreaChart { get; } = new();
    public ChartSampleViewModel HistogramChart { get; } = new();
    public ChartSampleViewModel BoxPlotChart { get; } = new();
    public ChartSampleViewModel ErrorBarChart { get; } = new();
    public ChartSampleViewModel PieChart { get; } = new();
    public ChartSampleViewModel DonutChart { get; } = new();
    public ChartSampleViewModel RadarChart { get; } = new();
    public ChartSampleViewModel TreeMapChart { get; } = new();
    public ChartSampleViewModel OrgChart { get; } = new();
    public ChartSampleViewModel ArcDiagramChart { get; } = new();
    public ChartSampleViewModel LiquidFillGauge { get; } = new();

    public AnimationViewModel()
    {
    }
}

public partial class ChartSampleViewModel : ObservableObject
{
    private readonly Random _random = new();

    public ObservableCollection<ChartDataItem> Series1Data { get; } = new();
    public ObservableCollection<ChartDataItem> Series2Data { get; } = new();
    public ObservableCollection<ChartDataItem> Series3Data { get; } = new();
    public ObservableCollection<HistogramItem> HistogramData { get; } = new();
    public ObservableCollection<BoxPlotItem> BoxPlotData { get; } = new();
    public ObservableCollection<ErrorBarItem> ErrorBarData { get; } = new();
    
    public ObservableCollection<HierarchicalItem> HierarchicalData { get; } = new();
    public ObservableCollection<OrgNodeItem> OrgData { get; } = new();
    public ObservableCollection<AnimationArcNode> ArcNodes { get; } = new();
    public ObservableCollection<AnimationArcLink> ArcLinks { get; } = new();

    [ObservableProperty]
    private TimeSpan _duration = TimeSpan.FromSeconds(1);

    [ObservableProperty]
    private string _durationText = "1.0";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Series1Delay))]
    [NotifyPropertyChangedFor(nameof(Series2Delay))]
    [NotifyPropertyChangedFor(nameof(Series3Delay))]
    private TimeSpan _stagger = TimeSpan.FromSeconds(0.3);

    [ObservableProperty]
    private string _staggerText = "0.3";

    [ObservableProperty]
    private double _liquidGaugeValue = 68;

    public TimeSpan Series1Delay => TimeSpan.Zero;
    public TimeSpan Series2Delay => Stagger;
    public TimeSpan Series3Delay => Stagger * 2;

    public ChartSampleViewModel()
    {
        GenerateInitialData();
    }

    private void GenerateInitialData()
    {
        var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun" };
        
        foreach (var month in months)
        {
            Series1Data.Add(new ChartDataItem(month, _random.Next(10, 50)));
            Series2Data.Add(new ChartDataItem(month, _random.Next(30, 80)));
            Series3Data.Add(new ChartDataItem(month, _random.Next(50, 90)));
        }

        foreach (var score in Enumerable.Range(0, 50).Select(_ => 50 + _random.NextDouble() * 50))
        {
            HistogramData.Add(new HistogramItem(score));
        }

        BoxPlotData.Add(new BoxPlotItem("Class A", 45, 55, 65, 75, 90));
        BoxPlotData.Add(new BoxPlotItem("Class B", 50, 62, 72, 82, 95));
        BoxPlotData.Add(new BoxPlotItem("Class C", 38, 48, 58, 68, 85));

        ErrorBarData.Add(new ErrorBarItem("A", 45, 5));
        ErrorBarData.Add(new ErrorBarItem("B", 62, 8));
        ErrorBarData.Add(new ErrorBarItem("C", 38, 4));
        ErrorBarData.Add(new ErrorBarItem("D", 75, 10));
        ErrorBarData.Add(new ErrorBarItem("E", 55, 6));

        // Hierarchical Data (TreeMap, etc.)
        HierarchicalData.Add(new HierarchicalItem("Engineering", 400, new List<HierarchicalItem>
        {
            new("Frontend", 150),
            new("Backend", 200),
            new("DevOps", 50)
        }));
        HierarchicalData.Add(new HierarchicalItem("Sales", 300, new List<HierarchicalItem>
        {
            new("Enterprise", 200),
            new("SMB", 100)
        }));
        HierarchicalData.Add(new HierarchicalItem("Marketing", 200));

        // Org Chart Data
        var ceo = new OrgNodeItem { Name = "CEO", Reports = new List<OrgNodeItem>() };
        ceo.Reports.Add(new OrgNodeItem { Name = "CTO", Reports = new List<OrgNodeItem> { new OrgNodeItem { Name = "Dev Lead" } } });
        ceo.Reports.Add(new OrgNodeItem { Name = "CFO", Reports = new List<OrgNodeItem> { new OrgNodeItem { Name = "Accounting" } } });
        OrgData.Add(ceo);

        ArcNodes.Add(new AnimationArcNode("A", "Design"));
        ArcNodes.Add(new AnimationArcNode("B", "API"));
        ArcNodes.Add(new AnimationArcNode("C", "Render"));
        ArcNodes.Add(new AnimationArcNode("D", "Test"));
        ArcNodes.Add(new AnimationArcNode("E", "Ship"));

        ArcLinks.Add(new AnimationArcLink("A", "B", 2));
        ArcLinks.Add(new AnimationArcLink("A", "C", 4));
        ArcLinks.Add(new AnimationArcLink("B", "D", 3));
        ArcLinks.Add(new AnimationArcLink("C", "E", 5));
        ArcLinks.Add(new AnimationArcLink("D", "E", 2));
    }

    [RelayCommand]
    private void UpdateData()
    {
        // Update values to trigger transition animation
        UpdateCollection(Series1Data);
        UpdateCollection(Series2Data);
        UpdateCollection(Series3Data);
        UpdateHistogramData();
        UpdateBoxPlotData();
        UpdateErrorBarData();
        UpdateArcDiagramData();
        UpdateLiquidGaugeData();
    }

    private void UpdateCollection(ObservableCollection<ChartDataItem> collection)
    {
        var count = collection.Count;
        for (int i = 0; i < count; i++)
        {
            var item = collection[i];
            var newVal = Math.Clamp(item.Value + _random.Next(-20, 20), 0, 100);
            collection[i] = new ChartDataItem(item.Category, newVal);
        }
    }

    private void UpdateHistogramData()
    {
        HistogramData.Clear();
        for (var i = 0; i < 50; i++)
        {
            HistogramData.Add(new HistogramItem(45 + _random.NextDouble() * 60));
        }
    }

    private void UpdateBoxPlotData()
    {
        for (var i = 0; i < BoxPlotData.Count; i++)
        {
            var item = BoxPlotData[i];
            var shift = _random.Next(-6, 7);
            BoxPlotData[i] = new BoxPlotItem(
                item.Class,
                Math.Clamp(item.Min + shift, 20, 100),
                Math.Clamp(item.Q1 + shift, 20, 100),
                Math.Clamp(item.Median + shift, 20, 100),
                Math.Clamp(item.Q3 + shift, 20, 100),
                Math.Clamp(item.Max + shift, 20, 100));
        }
    }

    private void UpdateErrorBarData()
    {
        for (var i = 0; i < ErrorBarData.Count; i++)
        {
            var item = ErrorBarData[i];
            ErrorBarData[i] = new ErrorBarItem(
                item.Sample,
                Math.Clamp(item.Value + _random.Next(-10, 11), 10, 95),
                Math.Clamp(item.Error + _random.Next(-2, 3), 2, 14));
        }
    }

    private void UpdateArcDiagramData()
    {
        for (var i = 0; i < ArcLinks.Count; i++)
        {
            var link = ArcLinks[i];
            ArcLinks[i] = link with { Value = Math.Clamp(link.Value + _random.Next(-2, 3), 1, 8) };
        }
    }

    private void UpdateLiquidGaugeData()
    {
        LiquidGaugeValue = Math.Clamp(LiquidGaugeValue + _random.Next(-18, 19), 8, 96);
    }

    partial void OnDurationTextChanged(string value)
    {
        if (double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var seconds))
        {
            Duration = TimeSpan.FromSeconds(seconds);
        }
    }

    partial void OnStaggerTextChanged(string value)
    {
        if (double.TryParse(value, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var seconds))
        {
            Stagger = TimeSpan.FromSeconds(seconds);
        }
    }
}

public record AnimationArcNode(string Id, string Label);

public record AnimationArcLink(string Source, string Target, double Value);


