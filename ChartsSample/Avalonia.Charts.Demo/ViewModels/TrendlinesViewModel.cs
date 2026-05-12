using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class TrendlinesViewModel : ViewModelBase
{
    public TrendlinesViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Linear Data (y ≈ 2.5x + 10)
        LinearData = new ObservableCollection<ChartDataPoint>
        {
            new("2018", 12),
            new("2019", 15),
            new("2020", 18),
            new("2021", 20),
            new("2022", 25)
        };

        // Exponential Data (rapid growth)
        ExponentialData = new ObservableCollection<ChartDataPoint>
        {
            new("Jan", 10),
            new("Feb", 15),
            new("Mar", 25),
            new("Apr", 45),
            new("May", 80),
            new("Jun", 150)
        };

        // Logarithmic Data (fast then slow growth)
        LogarithmicData = new ObservableCollection<ChartDataPoint>
        {
            new("1", 20),
            new("2", 45),
            new("3", 60),
            new("4", 70),
            new("5", 77),
            new("6", 82),
            new("7", 86),
            new("8", 89)
        };

        // Power Data (quadratic growth: y ≈ 5x²)
        PowerData = new ObservableCollection<ChartDataPoint>
        {
            new("1", 5),
            new("2", 20),
            new("3", 45),
            new("4", 80),
            new("5", 125),
            new("6", 180)
        };

        // Polynomial Data (Parabola: y = x² - 4x + 10)
        PolynomialData = new ObservableCollection<ChartDataPoint>
        {
            new("1", 10),
            new("2", 5),
            new("3", 2),
            new("4", 5),
            new("5", 10),
            new("6", 18),
            new("7", 30)
        };
        
        // Random Data for Moving Average
        RandomData = new ObservableCollection<ChartDataPoint>
        {
            new("Day 1", 50),
            new("Day 2", 65),
            new("Day 3", 45),
            new("Day 4", 75),
            new("Day 5", 60),
            new("Day 6", 85),
            new("Day 7", 70)
        };

        // Extended Random Data for MA comparison
        ExtendedRandomData = new ObservableCollection<ChartDataPoint>
        {
            new("1", 100),
            new("2", 108),
            new("3", 95),
            new("4", 115),
            new("5", 102),
            new("6", 120),
            new("7", 110),
            new("8", 130),
            new("9", 118),
            new("10", 140),
            new("11", 125),
            new("12", 145)
        };

        // Sales Data for multiple trendlines demo
        SalesData = new ObservableCollection<ChartDataPoint>
        {
            new("Q1", 45),
            new("Q2", 55),
            new("Q3", 48),
            new("Q4", 72),
            new("Q5", 65),
            new("Q6", 85),
            new("Q7", 78),
            new("Q8", 95)
        };

        // Forecast Data
        ForecastData = new ObservableCollection<ChartDataPoint>
        {
            new("2020", 50),
            new("2021", 65),
            new("2022", 80),
            new("2023", 95),
            new("2024", 115)
        };

        // Comparison Data (good for comparing all trendline types)
        ComparisonData = new ObservableCollection<ChartDataPoint>
        {
            new("1", 8),
            new("2", 18),
            new("3", 30),
            new("4", 42),
            new("5", 58),
            new("6", 72),
            new("7", 90),
            new("8", 105)
        };

        // Standalone overlay source data
        StandaloneTrendlineData = new ObservableCollection<ChartDataPoint>
        {
            new("1", 16),
            new("2", 20),
            new("3", 27),
            new("4", 29),
            new("5", 38),
            new("6", 43),
            new("7", 47),
            new("8", 56)
        };

        // Noisy Data for polynomial comparison
        NoisyData = new ObservableCollection<ChartDataPoint>
        {
            new("1", 12),
            new("2", 8),
            new("3", 22),
            new("4", 15),
            new("5", 35),
            new("6", 28),
            new("7", 50),
            new("8", 42),
            new("9", 65),
            new("10", 55)
        };
    }

    [ObservableProperty] private ObservableCollection<ChartDataPoint> _linearData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _exponentialData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _logarithmicData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _powerData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _polynomialData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _randomData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _extendedRandomData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _salesData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _forecastData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _comparisonData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _standaloneTrendlineData = null!;
    [ObservableProperty] private ObservableCollection<ChartDataPoint> _noisyData = null!;
}

public class ChartDataPoint
{
    public string Category { get; set; }
    public double Value { get; set; }

    public ChartDataPoint(string category, double value)
    {
        Category = category;
        Value = value;
    }
}
