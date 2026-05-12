using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class AnalyticsChartsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<WordItem> _wordCloudData = null!;

    [ObservableProperty]
    private List<ParliamentParty> _parliamentData = null!;

    [ObservableProperty]
    private ObservableCollection<CalendarItem> _calendarData = null!;

    [ObservableProperty]
    private ObservableCollection<FunnelItem> _funnelData = null!;

    [ObservableProperty]
    private ObservableCollection<PyramidItem> _pyramidData = null!;

    [ObservableProperty]
    private ObservableCollection<double> _sparklineData = null!;

    [ObservableProperty]
    private ObservableCollection<double> _sparklineWinLossData = null!;

    [ObservableProperty]
    private ObservableCollection<HeatmapItem> _heatmapData = null!;

    [ObservableProperty]
    private KpiItem _kpi1 = null!;

    [ObservableProperty]
    private KpiItem _kpi2 = null!;

    [ObservableProperty]
    private KpiItem _kpi3 = null!;

    [ObservableProperty]
    private ObservableCollection<MatrixItem> _matrixData = null!;

    [ObservableProperty]
    private ObservableCollection<string> _matrixColumns = null!;

    [ObservableProperty]
    private ObservableCollection<TableItem> _tableData = null!;

    [ObservableProperty]
    private ObservableCollection<TableChartColumn> _tableColumns = null!;

    [ObservableProperty]
    private ObservableCollection<MosaicItem> _mosaicData = null!;

    [ObservableProperty]
    private ObservableCollection<MatrixSparklineItem> _matrixSparklineData = null!;

    public AnalyticsChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Word Cloud
        WordCloudData = new ObservableCollection<WordItem>
        {
            new("Technology", 80),
            new("Innovation", 65),
            new("Digital", 55),
            new("Cloud", 50),
            new("AI", 70),
            new("Data", 60),
            new("Security", 45),
            new("Mobile", 40),
            new("Development", 35),
            new("Analytics", 30),
            new("Performance", 25),
            new("User", 45),
            new("Interface", 40)
        };

        // Parliament Chart
        ParliamentData = new List<ParliamentParty>
        {
            new() { Name = "Conservative", Seats = 140, Color = Color.FromRgb(0, 135, 220) },
            new() { Name = "Labour", Seats = 120, Color = Color.FromRgb(220, 36, 31) },
            new() { Name = "Liberal Democrats", Seats = 50, Color = Color.FromRgb(250, 166, 26) },
            new() { Name = "Green", Seats = 25, Color = Color.FromRgb(107, 171, 80) },
            new() { Name = "Independent", Seats = 15, Color = Color.FromRgb(128, 128, 128) }
        };

        // Calendar Heatmap
        var random = new Random(42);
        CalendarData = new ObservableCollection<CalendarItem>(
            Enumerable.Range(0, 365).Select(i => new CalendarItem(DateTime.Today.AddDays(-365 + i), random.Next(0, 10)))
        );

        // Funnel Chart
        FunnelData = new ObservableCollection<FunnelItem>
        {
            new("Visitors", 1000),
            new("Leads", 500),
            new("Qualified", 200),
            new("Proposal", 80),
            new("Closed", 30)
        };

        // Pyramid Chart
        PyramidData = new ObservableCollection<PyramidItem>
        {
            new("0-14", 15),
            new("15-24", 12),
            new("25-54", 40),
            new("55-64", 18),
            new("65+", 15)
        };

        // Sparklines
        SparklineData = new ObservableCollection<double> { 5, 10, 8, 15, 12, 20, 18, 25, 22, 30 };
        SparklineWinLossData = new ObservableCollection<double> { 1, -1, 1, 1, -1, 1, -1, -1, 1, 1 };

        // Heatmap
        const int size = 10;
        var heatmapItems = new List<HeatmapItem>();
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                double val = Math.Abs(Math.Sin(x * 0.5) * Math.Cos(y * 0.5) * 100);
                if (x == y) val = 100;
                heatmapItems.Add(new HeatmapItem($"R{x + 1}", $"C{y + 1}", val));
            }
        }
        HeatmapData = new ObservableCollection<HeatmapItem>(heatmapItems);

        // KPI Cards
        Kpi1 = new KpiItem(124500, "$", 12.5, "vs last month", new[] { 10.0, 12, 11, 14, 13, 15, 16, 14, 18, 20.0 });
        Kpi2 = new KpiItem(4532, null, 8.2, "New users", new[] { 50.0, 55, 52, 58, 60, 65, 62, 70, 75, 80.0 });
        Kpi3 = new KpiItem(24.5, "%", -2.1, "Bounce rate", new[] { 30.0, 28, 29, 27, 26, 25, 24, 25, 24, 24.5 }, Brushes.Green);

        // Matrix Chart
        MatrixColumns = new ObservableCollection<string> { "Grape", "Banana", "Orange", "Apple" };
        MatrixData = new ObservableCollection<MatrixItem>
        {
            new("Good for juicing", new[] { false, true, true, true }),
            new("Good for smoothies", new[] { true, true, false, true }),
            new("Good for baking", new[] { true, false, true, true }),
            new("Good for jam", new[] { true, false, false, true }),
            new("Good for salads", new[] { true, false, true, true })
        };

        // Table Chart
        TableColumns = new ObservableCollection<TableChartColumn>
        {
            new() { Header = "Avg Sales\n(units/mo)", ValuePath = "Sales", UseColorScale = true, MinValue = 100, MaxValue = 200, LowBrush = new SolidColorBrush(Color.FromRgb(220, 235, 255)), HighBrush = new SolidColorBrush(Color.FromRgb(33, 150, 243)) },
            new() { Header = "Avg Price\n($)", ValuePath = "Price", Format = "C1", UseColorScale = true, MinValue = 0.5, MaxValue = 3.0, LowBrush = new SolidColorBrush(Color.FromRgb(255, 235, 220)), HighBrush = new SolidColorBrush(Color.FromRgb(255, 152, 0)) },
            new() { Header = "Sweetness\n(0-10)", ValuePath = "Sweetness" },
            new() { Header = "Juiciness\n(0-10)", ValuePath = "Juiciness" },
            new() { Header = "Acidity\n(0-10)", ValuePath = "Acidity" },
        };
        TableData = new ObservableCollection<TableItem>
        {
            new("Apple", 200, 1.2, 6.8, 7.0, 4.5),
            new("Banana", 180, 0.5, 7.0, 8.5, 3.0),
            new("Orange", 150, 1.0, 5.5, 9.0, 6.0),
            new("Grape", 140, 2.5, 6.5, 6.0, 4.0),
            new("Pineapple", 130, 1.8, 6.0, 7.5, 5.0),
            new("Blueberry", 120, 3.0, 5.0, 6.5, 4.0),
            new("Mango", 110, 1.5, 8.5, 8.0, 2.5),
            new("Strawberry", 100, 2.0, 8.0, 5.0, 3.5)

        };

        // Mosaic Chart
        MosaicData = new ObservableCollection<MosaicItem>
        {
            new("North", "Electronics", 450),
            new("North", "Clothing", 320),
            new("North", "Home", 210),
            new("South", "Electronics", 380),
            new("South", "Clothing", 410),
            new("South", "Home", 180),
            new("East", "Electronics", 290),
            new("East", "Clothing", 250),
            new("East", "Home", 350),
            new("West", "Clothing", 280),
            new("West", "Home", 150)
        };

        // Matrix Sparkline (Table)
        MatrixSparklineData = new ObservableCollection<MatrixSparklineItem>
        {
            new("A", 12500, 8400, 1200, new[] { 10.0, 12, 11, 14, 13, 15, 16, 14, 18, 20.0 }),
            new("B", 15000, 11000, 2500, new[] { 50.0, 55, 52, 58, 60, 65, 62, 70, 75, 80.0 }),
            new("C", 9800, 9200, 500, new[] { 30.0, 28, 29, 27, 26, 25, 24, 25, 24, 24.5 }),
            new("D", 18200, 12400, 3200, new[] { 40.0, 42, 38, 45, 48, 50, 45, 55, 60, 65.0 }),
            new("E", 7500, 6800, 100, new[] { 20.0, 18, 22, 19, 24, 21, 23, 25, 22, 28.0 })
        };
    }
}

public record MatrixSparklineItem(string Name, double Income, double Expenses, double Debt, double[] Trend);

public record MosaicItem(string Region, string Category, double Sales);

public record WordItem(string Word, double Count);
public record CalendarItem(DateTime Date, int Count);
public record FunnelItem(string Stage, double Value);
public record PyramidItem(string Age, double Value);
public record HeatmapItem(string Row, string Col, double Val);
public record KpiItem(double Value, string? Unit, double Delta, string Subtitle, double[] SparklineData, IBrush? NegativeBrush = null);
public record MatrixItem(string Attribute, bool[] Values);
public record TableItem(string Product, double Sales, double Price, double Sweetness, double Juiciness, double Acidity);
