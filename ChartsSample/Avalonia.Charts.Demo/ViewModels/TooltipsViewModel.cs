using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class TooltipsViewModel : ViewModelBase
{
    public TooltipsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Basic Tooltips
        BasicTooltipData = new ObservableCollection<SalesData>
        {
            new() { Month = "Jan", Value = 45 },
            new() { Month = "Feb", Value = 52 },
            new() { Month = "Mar", Value = 38 },
            new() { Month = "Apr", Value = 65 },
            new() { Month = "May", Value = 70 },
            new() { Month = "Jun", Value = 55 }
        };

        // Per-Series Tooltips
        Series1Data = new ObservableCollection<double> { 120, 150, 180, 200 };
        Series2Data = new ObservableCollection<double> { 140, 170, 160, 220 };

        // Custom Template Tooltips
        CustomTooltipData = new ObservableCollection<ProductData>
        {
            new() { Name = "Widget A", UnitsSold = 150, Price = 29.99 },
            new() { Name = "Widget B", UnitsSold = 230, Price = 49.99 },
            new() { Name = "Widget C", UnitsSold = 180, Price = 19.99 },
            new() { Name = "Widget D", UnitsSold = 95, Price = 99.99 }
        };
    }

    [ObservableProperty] private ObservableCollection<SalesData> _basicTooltipData = null!;
    [ObservableProperty] private ObservableCollection<double> _series1Data = null!;
    [ObservableProperty] private ObservableCollection<double> _series2Data = null!;
    [ObservableProperty] private ObservableCollection<ProductData> _customTooltipData = null!;
}

public class SalesData
{
    public string Month { get; set; } = string.Empty;
    public double Value { get; set; }
}

public class ProductData
{
    public string Name { get; set; } = string.Empty;
    public int UnitsSold { get; set; }
    public double Price { get; set; }
    public double Revenue => UnitsSold * Price;
}
