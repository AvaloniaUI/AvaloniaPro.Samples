using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Charts.Demo.ViewModels.Applications;

public partial class SalesDashboardViewModel : PageViewModel
{
    [ObservableProperty] private double _totalRevenue = 128450.00;
    [ObservableProperty] private double _revenueChange = 12.5;
    [ObservableProperty] private int _totalOrders = 1240;
    [ObservableProperty] private double _ordersChange = 8.2;
    [ObservableProperty] private double _averageOrderValue = 103.50;
    [ObservableProperty] private double _aovChange = -2.1;
    [ObservableProperty] private int _totalCustomers = 854;
    [ObservableProperty] private double _customersChange = 15.3;
    [ObservableProperty] private string _searchText = "";
    
    // Sparkline data for KPI trends
    public ObservableCollection<SparklinePoint> RevenueSparkline { get; } = new();
    public ObservableCollection<SparklinePoint> OrdersSparkline { get; } = new();
    public ObservableCollection<SparklinePoint> AovSparkline { get; } = new();
    public ObservableCollection<SparklinePoint> CustomersSparkline { get; } = new();
    
    // Regional sales data for map
    public ObservableCollection<RegionSales> RegionalSales { get; } = new();
    
    // Radar chart data for product performance
    public ObservableCollection<ObservableCollection<RadarData>> ProductPerformanceData { get; } = new();
    
    private string _selectedPeriod = "This Month";
    public string SelectedPeriod
    {
        get => _selectedPeriod;
        set
        {
            if (SetProperty(ref _selectedPeriod, value))
            {
                UpdateDataForPeriod(value);
            }
        }
    }

    public SalesDashboardViewModel() : base("Sales Dashboard")
    {
        // Time periods for filter
        TimePeriods = new ObservableCollection<string> { "Today", "This Week", "This Month", "This Year" };
        
        // Initialize collections
        MonthlyRevenue = new ObservableCollection<SalesPoint>();
        CategoryData = new ObservableCollection<CategorySales>();
        RecentOrders = new ObservableCollection<Order>();
        TopProducts = new ObservableCollection<TopProduct>();
        
        // Initialize Regional Sales data
        InitializeRegionalSales();
        
        // Initialize Radar Chart data
        InitializeRadarData();
        
        // Load initial data
        UpdateDataForPeriod("This Month");
    }
    
    private void InitializeRegionalSales()
    {
        RegionalSales.Add(new RegionSales("United States", "US", 37.09, -95.71, 45200, 15.2, "#3B82F6"));
        RegionalSales.Add(new RegionSales("Germany", "DE", 51.17, 10.45, 38400, 12.8, "#8B5CF6"));
        RegionalSales.Add(new RegionSales("China", "CN", 35.86, 104.2, 28900, 15.5, "#10B981"));
        RegionalSales.Add(new RegionSales("Brazil", "BR", -14.24, -51.93, 12800, 8.3, "#F59E0B"));
        RegionalSales.Add(new RegionSales("Australia", "AU", -25.27, 133.78, 9600, 18.7, "#EC4899"));
        RegionalSales.Add(new RegionSales("South Africa", "ZA", -30.56, 22.94, 5400, 25.1, "#6366F1"));
    }
    
    private void InitializeRadarData()
    {
        // Electronics performance
        var electronics = new ObservableCollection<RadarData>
        {
            new("Sales", 95),
            new("Growth", 88),
            new("Profit", 92),
            new("Returns", 15),
            new("Reviews", 90),
            new("Stock", 85)
        };
        
        // Fashion performance
        var fashion = new ObservableCollection<RadarData>
        {
            new("Sales", 78),
            new("Growth", 92),
            new("Profit", 75),
            new("Returns", 25),
            new("Reviews", 82),
            new("Stock", 90)
        };
        
        // Home & Garden performance
        var home = new ObservableCollection<RadarData>
        {
            new("Sales", 65),
            new("Growth", 70),
            new("Profit", 80),
            new("Returns", 12),
            new("Reviews", 88),
            new("Stock", 72)
        };
        
        ProductPerformanceData.Add(electronics);
        ProductPerformanceData.Add(fashion);
        ProductPerformanceData.Add(home);
    }

    private void UpdateDataForPeriod(string period)
    {
        switch (period)
        {
            case "Today":
                LoadTodayData();
                break;
            case "This Week":
                LoadThisWeekData();
                break;
            case "This Month":
                LoadThisMonthData();
                break;
            case "This Year":
                LoadThisYearData();
                break;
        }
    }

    private void UpdateSparklines(double[] revenue, int[] orders, double[] aov, int[] customers)
    {
        RevenueSparkline.Clear();
        OrdersSparkline.Clear();
        AovSparkline.Clear();
        CustomersSparkline.Clear();

        for (int i = 0; i < revenue.Length; i++)
            RevenueSparkline.Add(new SparklinePoint(i, revenue[i]));
        for (int i = 0; i < orders.Length; i++)
            OrdersSparkline.Add(new SparklinePoint(i, orders[i]));
        for (int i = 0; i < aov.Length; i++)
            AovSparkline.Add(new SparklinePoint(i, aov[i]));
        for (int i = 0; i < customers.Length; i++)
            CustomersSparkline.Add(new SparklinePoint(i, customers[i]));
    }

    private void LoadTodayData()
    {
        TotalRevenue = 4850.00;
        RevenueChange = 5.2;
        TotalOrders = 45;
        OrdersChange = 12.5;
        AverageOrderValue = 107.78;
        AovChange = -1.5;
        TotalCustomers = 38;
        CustomersChange = 8.6;

        MonthlyRevenue.Clear();
        var hours = new[] { "8AM", "10AM", "12PM", "2PM", "4PM", "6PM", "8PM" };
        var values = new[] { 320.0, 580.0, 920.0, 750.0, 680.0, 890.0, 710.0 };
        var targets = new[] { 300.0, 500.0, 850.0, 800.0, 750.0, 850.0, 750.0 };
        for (int i = 0; i < hours.Length; i++)
            MonthlyRevenue.Add(new SalesPoint(hours[i], values[i], targets[i]));

        // Update sparklines with hourly trend data
        UpdateSparklines(
            new[] { 280.0, 450.0, 680.0, 920.0, 850.0, 780.0, 890.0, 710.0 },
            new[] { 38, 42, 48, 52, 49, 51, 55, 58 },
            new[] { 98.5, 102.3, 105.8, 107.2, 106.5, 108.1, 107.8, 107.78 },
            new[] { 32, 34, 35, 36, 37, 37, 38, 38 }
        );

        UpdateCategoryData(new[] { 35.0, 25.0, 22.0, 12.0, 6.0 });
        UpdateRecentOrders(3);
    }

    private void LoadThisWeekData()
    {
        TotalRevenue = 28450.00;
        RevenueChange = 8.7;
        TotalOrders = 268;
        OrdersChange = 6.3;
        AverageOrderValue = 106.16;
        AovChange = 2.1;
        TotalCustomers = 210;
        CustomersChange = 11.2;

        MonthlyRevenue.Clear();
        var days = new[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };
        var values = new[] { 3200.0, 4100.0, 3800.0, 5200.0, 4800.0, 6100.0, 4250.0 };
        var targets = new[] { 3500.0, 3800.0, 4000.0, 4500.0, 5000.0, 5500.0, 5000.0 };
        for (int i = 0; i < days.Length; i++)
            MonthlyRevenue.Add(new SalesPoint(days[i], values[i], targets[i]));

        // Update sparklines with daily trend data
        UpdateSparklines(
            new[] { 2800.0, 3200.0, 4100.0, 3800.0, 5200.0, 4800.0, 6100.0, 4250.0 },
            new[] { 245, 268, 275, 262, 298, 285, 312, 268 },
            new[] { 104.2, 105.5, 103.8, 106.2, 104.8, 107.5, 106.1, 106.16 },
            new[] { 185, 192, 198, 204, 208, 212, 218, 210 }
        );

        UpdateCategoryData(new[] { 38.0, 23.0, 20.0, 13.0, 6.0 });
        UpdateRecentOrders(5);
    }

    private void LoadThisMonthData()
    {
        TotalRevenue = 128450.00;
        RevenueChange = 12.5;
        TotalOrders = 1240;
        OrdersChange = 8.2;
        AverageOrderValue = 103.50;
        AovChange = -2.1;
        TotalCustomers = 854;
        CustomersChange = 15.3;

        MonthlyRevenue.Clear();
        var months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        var values = new[] { 12000.0, 15000.0, 13000.0, 18000.0, 22000.0, 20000.0, 28450.0, 25000.0, 22000.0, 26000.0, 30000.0, 35000.0 };
        var targets = new[] { 11000.0, 13000.0, 14000.0, 16000.0, 18000.0, 21000.0, 25000.0, 26000.0, 24000.0, 28000.0, 32000.0, 33000.0 };
        for (int i = 0; i < months.Length; i++)
            MonthlyRevenue.Add(new SalesPoint(months[i], values[i], targets[i]));

        // Update sparklines with monthly trend data
        UpdateSparklines(
            new[] { 9800.0, 11200.0, 12500.0, 11800.0, 14200.0, 16500.0, 15800.0, 17200.0, 18500.0, 19800.0, 21500.0, 22800.0 },
            new[] { 1050, 1125, 1080, 1155, 1220, 1180, 1240, 1210, 1265, 1300, 1345, 1240 },
            new[] { 101.2, 102.5, 104.8, 103.2, 105.5, 104.8, 106.2, 105.8, 107.2, 106.5, 107.8, 108.1 },
            new[] { 685, 720, 758, 792, 825, 818, 854, 892, 935, 975, 1018, 854 }
        );

        UpdateCategoryData(new[] { 35.0, 25.0, 22.0, 12.0, 6.0 });
        UpdateRecentOrders(5);
    }

    private void LoadThisYearData()
    {
        TotalRevenue = 284350.00;
        RevenueChange = 22.8;
        TotalOrders = 2840;
        OrdersChange = 18.5;
        AverageOrderValue = 100.12;
        AovChange = 3.5;
        TotalCustomers = 1850;
        CustomersChange = 28.6;

        MonthlyRevenue.Clear();
        var years = new[] { "2020", "2021", "2022", "2023", "2024" };
        var values = new[] { 125000.0, 158000.0, 192000.0, 231000.0, 284350.0 };
        var targets = new[] { 120000.0, 150000.0, 180000.0, 220000.0, 270000.0 };
        for (int i = 0; i < years.Length; i++)
            MonthlyRevenue.Add(new SalesPoint(years[i], values[i], targets[i]));

        // Update sparklines with yearly trend data
        UpdateSparklines(
            new[] { 95000.0, 108000.0, 125000.0, 142000.0, 158000.0, 172000.0, 192000.0, 208000.0, 231000.0, 248000.0, 268000.0, 284350.0 },
            new[] { 2180, 2250, 2390, 2450, 2580, 2620, 2750, 2780, 2890, 2950, 3120, 2840 },
            new[] { 95.5, 96.2, 97.8, 98.5, 99.2, 99.8, 100.5, 101.2, 102.5, 103.2, 104.8, 100.12 },
            new[] { 1250, 1320, 1405, 1480, 1562, 1648, 1725, 1820, 1925, 2015, 2120, 1850 }
        );

        UpdateCategoryData(new[] { 32.0, 28.0, 18.0, 15.0, 7.0 });
        UpdateRecentOrders(8);
    }

    private void UpdateCategoryData(double[] percentages)
    {
        CategoryData.Clear();
        var total = TotalRevenue;
        CategoryData.Add(new CategorySales("Electronics", total * percentages[0] / 100, "#3B82F6", (int)percentages[0]));
        CategoryData.Add(new CategorySales("Fashion", total * percentages[1] / 100, "#EC4899", (int)percentages[1]));
        CategoryData.Add(new CategorySales("Home & Garden", total * percentages[2] / 100, "#10B981", (int)percentages[2]));
        CategoryData.Add(new CategorySales("Sports", total * percentages[3] / 100, "#F59E0B", (int)percentages[3]));
        CategoryData.Add(new CategorySales("Others", total * percentages[4] / 100, "#6B7280", (int)percentages[4]));
    }

    private void UpdateRecentOrders(int count)
    {
        RecentOrders.Clear();
        var orders = new[]
        {
            new Order("#ORD-001", "John Smith", "Electronics", 1250.00, "Completed", DateTime.Now.AddHours(-2)),
            new Order("#ORD-002", "Sarah Johnson", "Fashion", 340.50, "Processing", DateTime.Now.AddHours(-4)),
            new Order("#ORD-003", "Mike Brown", "Home & Garden", 890.00, "Completed", DateTime.Now.AddHours(-6)),
            new Order("#ORD-004", "Emily Davis", "Electronics", 2100.00, "Pending", DateTime.Now.AddHours(-8)),
            new Order("#ORD-005", "Chris Wilson", "Sports", 150.00, "Completed", DateTime.Now.AddHours(-12)),
            new Order("#ORD-006", "Anna Lee", "Fashion", 520.00, "Processing", DateTime.Now.AddHours(-16)),
            new Order("#ORD-007", "David Miller", "Electronics", 1890.00, "Completed", DateTime.Now.AddHours(-20)),
            new Order("#ORD-008", "Lisa Garcia", "Home & Garden", 425.00, "Pending", DateTime.Now.AddHours(-24))
        };
        
        for (int i = 0; i < count && i < orders.Length; i++)
            RecentOrders.Add(orders[i]);

        // Update Top Products
        UpdateTopProducts();
    }

    private void UpdateTopProducts()
    {
        TopProducts.Clear();
        var products = new[]
        {
            new TopProduct("iPhone 14 Pro", "Electronics", 450, 1250.00, 562500),
            new TopProduct("MacBook Pro M2", "Electronics", 120, 2200.00, 264000),
            new TopProduct("Samsung Galaxy S23", "Electronics", 210, 999.00, 209790),
            new TopProduct("Nike Air Max", "Fashion", 380, 150.00, 57000),
            new TopProduct("Adidas Ultraboost", "Fashion", 290, 180.00, 52200)
        };
        
        foreach (var product in products)
            TopProducts.Add(product);
    }

    public ObservableCollection<string> TimePeriods { get; }
    public ObservableCollection<SalesPoint> MonthlyRevenue { get; }
    public ObservableCollection<CategorySales> CategoryData { get; }
    public ObservableCollection<Order> RecentOrders { get; }
    public ObservableCollection<TopProduct> TopProducts { get; }
}

public record SalesPoint(string Month, double Value, double Target);
public record CategorySales(string Category, double Value, string Color, int Percentage);
public record Order(string OrderId, string Customer, string Category, double Amount, string Status, DateTime Date);
public record TopProduct(string Name, string Category, int Quantity, double Price, double Total);
public record SparklinePoint(int Index, double Value);
public record RegionSales(string Region, string Code, double Latitude, double Longitude, double Sales, double Growth, string Color);
public record RadarData(string Label, double Value);
