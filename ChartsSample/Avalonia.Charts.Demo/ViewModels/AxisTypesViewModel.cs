using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class AxisTypesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<XYItem> _numericalData = null!;

    [ObservableProperty]
    private ObservableCollection<CategoryItem> _categoryData = null!;

    [ObservableProperty]
    private ObservableCollection<DateTimeItem> _dateTimeData = null!;

    [ObservableProperty]
    private ObservableCollection<double> _logData = null!;

    public AxisTypesViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Numerical Chart
        NumericalData = new ObservableCollection<XYItem>
        {
            new(10, 50),
            new(20, 80),
            new(30, 40),
            new(40, 100),
            new(50, 60),
            new(60, 90)
        };

        // Category Chart
        CategoryData = new ObservableCollection<CategoryItem>
        {
            new("Sales", 150),
            new("IT", 180),
            new("HR", 165),
            new("Marketing", 190)
        };

        // DateTime Chart
        var now = DateTime.Today;
        DateTimeData = new ObservableCollection<DateTimeItem>
        {
            new(now, 10),
            new(now.AddDays(1), 15),
            new(now.AddDays(2), 12),
            new(now.AddDays(3), 20),
            new(now.AddDays(4), 18),
            new(now.AddDays(5), 25),
            new(now.AddDays(6), 22)
        };

        // Log Chart
        LogData = new ObservableCollection<double> { 10, 100, 1000, 10000, 100000 };
    }
}

public record XYItem(double X, double Y);
public record CategoryItem(string Name, double Value);
public record DateTimeItem(DateTime Date, double Value);
