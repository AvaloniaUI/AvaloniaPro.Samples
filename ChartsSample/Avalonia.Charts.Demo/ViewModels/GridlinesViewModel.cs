using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class GridlinesViewModel : ViewModelBase
{
    public ObservableCollection<double> DefaultGridData { get; }
    public ObservableCollection<double> StyledGridData { get; }
    public ObservableCollection<object> MinorGridData { get; }
    public ObservableCollection<double> NoGridData { get; }

    public GridlinesViewModel()
    {
        DefaultGridData = new ObservableCollection<double> { 10, 25, 15, 40, 30, 50 };
        StyledGridData = new ObservableCollection<double> { 50, 80, 45, 90, 60 };
        MinorGridData = new ObservableCollection<object>
        {
            new { X = 0, Y = 2.2 },
            new { X = 1, Y = 4.8 },
            new { X = 2, Y = 3.1 },
            new { X = 3, Y = 6.9 },
            new { X = 4, Y = 5.2 },
            new { X = 5, Y = 8.4 },
            new { X = 6, Y = 7.1 }
        };
        NoGridData = new ObservableCollection<double> { 5, 20, 10, 30, 25, 15 };
    }
}
