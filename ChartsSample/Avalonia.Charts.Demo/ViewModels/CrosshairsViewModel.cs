using System;
using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class CrosshairsViewModel : ViewModelBase
{
    public ObservableCollection<double> CrosshairData { get; }
    public ObservableCollection<ContinuousDatePoint> ContinuousDateTimeData { get; }

    public CrosshairsViewModel()
    {
        CrosshairData = new ObservableCollection<double> 
        { 
            18.5, 21.2, 24.8, 22.1, 19.7, 23.4, 26.1 
        };

        var start = new DateTime(2026, 4, 1, 9, 0, 0);
        ContinuousDateTimeData = new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 42),
            new(start.AddHours(3), 45),
            new(start.AddHours(9), 39),
            new(start.AddDays(1).AddHours(4), 54),
            new(start.AddDays(2), 51),
            new(start.AddDays(4).AddHours(6), 63)
        };
    }
}
