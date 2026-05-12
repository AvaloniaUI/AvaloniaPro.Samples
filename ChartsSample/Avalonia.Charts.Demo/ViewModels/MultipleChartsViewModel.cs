using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class MultipleChartsViewModel : ViewModelBase
{
    public MultipleChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Grouped Bar Chart
        GroupedBar2022 = new ObservableCollection<int> { 127, 124, 107, 88 };
        GroupedBar2023 = new ObservableCollection<int> { 130, 93, 106, 96 };
        GroupedBar2024 = new ObservableCollection<int> { 100, 95, 90, 72 };

        // Bar + Line Combo
        BarLineComboData = new ObservableCollection<int> { 127, 92, 105, 68 };

        // Multi Line Chart
        ProductAData = new ObservableCollection<int> { 45, 52, 58, 65, 72, 68, 75 };
        ProductBData = new ObservableCollection<int> { 30, 38, 35, 45, 50, 55, 52 };
        ProductCData = new ObservableCollection<int> { 20, 25, 28, 32, 38, 42, 48 };

        // Stacked Line Combo
        OnlineData = new ObservableCollection<int> { 40, 55, 65, 50, 70 };
        InStoreData = new ObservableCollection<int> { 60, 45, 55, 70, 65 };
        TotalData = new ObservableCollection<double>();
        for (int i = 0; i < OnlineData.Count; i++)
        {
            TotalData.Add(OnlineData[i] + InStoreData[i]);
        }

        // Area + Line Combo
        TrafficData = new ObservableCollection<int> { 1200, 1400, 1100, 1600, 1800, 1500, 2000 };
        GoalData = new ObservableCollection<int> { 1500, 1500, 1500, 1500, 1500, 1500, 1500 };

        // Combo Chart Demo
        RevenueData = new ObservableCollection<int> { 150, 200, 180, 220, 250 };
        TargetData = new ObservableCollection<int> { 140, 190, 170, 230, 260 };

        // Grouped Bar (Product Breakdown)
        MatchaLatteData = new ObservableCollection<ProductSale>
        {
            new() { Year = "2012", Sales = 41.1 },
            new() { Year = "2013", Sales = 30.4 },
            new() { Year = "2014", Sales = 65.1 },
            new() { Year = "2015", Sales = 53.3 },
            new() { Year = "2016", Sales = 83.8 },
            new() { Year = "2017", Sales = 98.7 }
        };
        MilkTeaData = new ObservableCollection<ProductSale>
        {
            new() { Year = "2012", Sales = 86.5 },
            new() { Year = "2013", Sales = 92.1 },
            new() { Year = "2014", Sales = 85.7 },
            new() { Year = "2015", Sales = 83.1 },
            new() { Year = "2016", Sales = 73.4 },
            new() { Year = "2017", Sales = 55.1 }
        };
        CheeseCocoaData = new ObservableCollection<ProductSale>
        {
            new() { Year = "2012", Sales = 24.1 },
            new() { Year = "2013", Sales = 67.2 },
            new() { Year = "2014", Sales = 79.5 },
            new() { Year = "2015", Sales = 86.4 },
            new() { Year = "2016", Sales = 65.2 },
            new() { Year = "2017", Sales = 82.5 }
        };
        WalnutBrownieData = new ObservableCollection<ProductSale>
        {
            new() { Year = "2012", Sales = 55.2 },
            new() { Year = "2013", Sales = 67.1 },
            new() { Year = "2014", Sales = 69.2 },
            new() { Year = "2015", Sales = 72.4 },
            new() { Year = "2016", Sales = 53.9 },
            new() { Year = "2017", Sales = 39.1 }
        };
    }

    // Grouped Bar Chart
    [ObservableProperty] private ObservableCollection<int> _groupedBar2022 = null!;
    [ObservableProperty] private ObservableCollection<int> _groupedBar2023 = null!;
    [ObservableProperty] private ObservableCollection<int> _groupedBar2024 = null!;

    // Bar + Line Combo
    [ObservableProperty] private ObservableCollection<int> _barLineComboData = null!;

    // Multi Line Chart
    [ObservableProperty] private ObservableCollection<int> _productAData = null!;
    [ObservableProperty] private ObservableCollection<int> _productBData = null!;
    [ObservableProperty] private ObservableCollection<int> _productCData = null!;

    // Stacked Line Combo
    [ObservableProperty] private ObservableCollection<int> _onlineData = null!;
    [ObservableProperty] private ObservableCollection<int> _inStoreData = null!;
    [ObservableProperty] private ObservableCollection<double> _totalData = null!;

    // Area + Line Combo
    [ObservableProperty] private ObservableCollection<int> _trafficData = null!;
    [ObservableProperty] private ObservableCollection<int> _goalData = null!;

    // Combo Chart Demo
    [ObservableProperty] private ObservableCollection<int> _revenueData = null!;
    [ObservableProperty] private ObservableCollection<int> _targetData = null!;

    // Grouped Bar (Product Breakdown)
    [ObservableProperty] private ObservableCollection<ProductSale> _matchaLatteData = null!;
    [ObservableProperty] private ObservableCollection<ProductSale> _milkTeaData = null!;
    [ObservableProperty] private ObservableCollection<ProductSale> _cheeseCocoaData = null!;
    [ObservableProperty] private ObservableCollection<ProductSale> _walnutBrownieData = null!;
}

public class ProductSale
{
    public string Year { get; set; } = string.Empty;
    public double Sales { get; set; }
}
