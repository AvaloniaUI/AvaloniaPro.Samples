using System;
using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class TrackballViewModel : ViewModelBase
{
    public ObservableCollection<TrackballDataPoint> Data1 { get; }
    public ObservableCollection<TrackballDataPoint> Data2 { get; }
    public ObservableCollection<ContinuousDatePoint> ContinuousData1 { get; }
    public ObservableCollection<ContinuousDatePoint> ContinuousData2 { get; }

    public TrackballViewModel()
    {
        Data1 = new ObservableCollection<TrackballDataPoint>
        {
            new("Jan", 35), new("Feb", 28), new("Mar", 34), new("Apr", 32),
            new("May", 40), new("Jun", 35), new("Jul", 55)
        };

        Data2 = new ObservableCollection<TrackballDataPoint>
        {
            new("Jan", 25), new("Feb", 38), new("Mar", 14), new("Apr", 42),
            new("May", 30), new("Jun", 45), new("Jul", 35)
        };

        var start = new DateTime(2026, 4, 2);
        ContinuousData1 = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 28),
            new(start.AddDays(1), 36),
            new(start.AddDays(4), 32),
            new(start.AddDays(9), 58),
            new(start.AddDays(15), 52)
        };

        ContinuousData2 = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 22),
            new(start.AddDays(1), 31),
            new(start.AddDays(4), 37),
            new(start.AddDays(9), 49),
            new(start.AddDays(15), 61)
        };
    }
}

public class TrackballDataPoint
{
    public string Category { get; }
    public double Value { get; }

    public TrackballDataPoint(string category, double value)
    {
        Category = category;
        Value = value;
    }
}
