using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class DataPathViewModel : ViewModelBase
{
    public ObservableCollection<SimplePathPoint> SimplePathData { get; } =
    [
        new("Jan", 42),
        new("Feb", 58),
        new("Mar", 73),
        new("Apr", 64),
        new("May", 91)
    ];

    public ObservableCollection<NestedPathPoint> NestedPathData { get; } =
    [
        new(new RegionInfo("North"), new RevenueMetrics(120, 0.08)),
        new(new RegionInfo("South"), new RevenueMetrics(86, 0.04)),
        new(new RegionInfo("East"), new RevenueMetrics(142, 0.12)),
        new(new RegionInfo("West"), new RevenueMetrics(104, 0.06))
    ];

    public ObservableCollection<IndexedPathPoint> IndexedPathData { get; } =
    [
        new([new SegmentInfo("Core")], new IndexedMetrics([72, 94])),
        new([new SegmentInfo("Growth")], new IndexedMetrics([68, 112])),
        new([new SegmentInfo("Enterprise")], new IndexedMetrics([88, 128])),
        new([new SegmentInfo("Support")], new IndexedMetrics([52, 76]))
    ];

    public ObservableCollection<Dictionary<string, object>> DictionaryPathData { get; } =
    [
        CreateDictionaryPoint("Alpha", 32, 48),
        CreateDictionaryPoint("Beta", 44, 66),
        CreateDictionaryPoint("Gamma", 51, 79),
        CreateDictionaryPoint("Delta", 39, 57)
    ];

    private static Dictionary<string, object> CreateDictionaryPoint(string name, double previous, double current)
    {
        return new Dictionary<string, object>
        {
            ["Profile"] = new Dictionary<string, object>
            {
                ["Name"] = name
            },
            ["Metrics"] = new Dictionary<string, object>
            {
                ["History"] = new[]
                {
                    new Dictionary<string, object> { ["Value"] = previous },
                    new Dictionary<string, object> { ["Value"] = current }
                }
            }
        };
    }
}

public sealed record SimplePathPoint(string Month, double Sales);

public sealed record NestedPathPoint(RegionInfo Region, RevenueMetrics Metrics);

public sealed record RegionInfo(string Name);

public sealed record RevenueMetrics(double Revenue, double Growth);

public sealed record IndexedPathPoint(SegmentInfo[] Groups, IndexedMetrics Metrics);

public sealed record SegmentInfo(string Name);

public sealed record IndexedMetrics(double[] Values);
