using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class ContinuousXAxisViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ContinuousDatePoint> _dateTimeLineData = null!;

    [ObservableProperty]
    private ObservableCollection<ContinuousDatePoint> _dateTimeAreaData = null!;

    [ObservableProperty]
    private ObservableCollection<ContinuousDatePoint> _dateTimeStepData = null!;

    [ObservableProperty]
    private ObservableCollection<ContinuousDatePoint> _dateTimeSplineData = null!;

    [ObservableProperty]
    private ObservableCollection<ContinuousDatePoint> _dateTimeSplineAreaData = null!;

    [ObservableProperty]
    private ObservableCollection<ContinuousNumericPoint> _numericLineData = null!;

    [ObservableProperty]
    private ObservableCollection<ContinuousNumericPoint> _logarithmicLineData = null!;

    [ObservableProperty]
    private ObservableCollection<ContinuousDatePoint> _categoricalComparisonData = null!;

    public ContinuousXAxisViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        var start = new DateTime(2026, 4, 2);

        DateTimeLineData = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 28),
            new(start.AddDays(1), 36),
            new(start.AddDays(4), 32),
            new(start.AddDays(10), 58),
            new(start.AddDays(14), 51),
            new(start.AddDays(25), 74)
        };

        DateTimeAreaData = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 18),
            new(start.AddDays(1), 24),
            new(start.AddDays(4), 22),
            new(start.AddDays(10), 44),
            new(start.AddDays(14), 38),
            new(start.AddDays(25), 57)
        };

        DateTimeStepData = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 30),
            new(start.AddDays(1), 42),
            new(start.AddDays(4), 42),
            new(start.AddDays(10), 60),
            new(start.AddDays(14), 55),
            new(start.AddDays(25), 68)
        };

        DateTimeSplineData = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 62),
            new(start.AddDays(2), 54),
            new(start.AddDays(5), 71),
            new(start.AddDays(11), 66),
            new(start.AddDays(17), 82),
            new(start.AddDays(25), 76)
        };

        DateTimeSplineAreaData = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 34),
            new(start.AddDays(2), 40),
            new(start.AddDays(5), 36),
            new(start.AddDays(11), 51),
            new(start.AddDays(17), 47),
            new(start.AddDays(25), 64)
        };

        NumericLineData = new ObservableCollection<ContinuousNumericPoint>
        {
            new(5, 16),
            new(6.5, 26),
            new(8, 22),
            new(13, 45),
            new(18, 39),
            new(26, 63),
            new(40, 72)
        };

        LogarithmicLineData = new ObservableCollection<ContinuousNumericPoint>
        {
            new(1, 18),
            new(2, 24),
            new(5, 33),
            new(10, 45),
            new(50, 61),
            new(100, 70),
            new(500, 84),
            new(1000, 92)
        };

        CategoricalComparisonData = new ObservableCollection<ContinuousDatePoint>(DateTimeLineData);
    }
}

public record ContinuousDatePoint(DateTime Date, double Value);
public record ContinuousNumericPoint(double X, double Y);
