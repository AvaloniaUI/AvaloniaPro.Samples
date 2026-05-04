using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class FinancialChartsViewModel : ViewModelBase
{
    public FinancialChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Candlestick Chart (50 points)
        CandlestickData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(50));
        StyledCandlestickData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(18, seed: 84));

        // OHLC Chart (30 points)
        OhlcData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(30));
        StyledOhlcData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(14, seed: 126));

        // Hilo Chart (30 points)
        HiloData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(30));

        // Renko Chart
        RenkoData = new ObservableCollection<RenkoPoint>
        {
            new RenkoPoint(100.0), new RenkoPoint(105.0), new RenkoPoint(103.0),
            new RenkoPoint(108.0), new RenkoPoint(115.0), new RenkoPoint(112.0),
            new RenkoPoint(118.0), new RenkoPoint(120.0), new RenkoPoint(115.0),
            new RenkoPoint(122.0),
            // Add a down trend to show red bricks
            new RenkoPoint(118.0), new RenkoPoint(114.0), new RenkoPoint(110.0),
            new RenkoPoint(105.0), new RenkoPoint(100.0)
        };

        // Waterfall Chart
        WaterfallData = new ObservableCollection<WaterfallFinancialPoint>
        {
            new WaterfallFinancialPoint("Revenue", 500.0),
            new WaterfallFinancialPoint("COGS", -200.0),
            new WaterfallFinancialPoint("Marketing", -50.0),
            new WaterfallFinancialPoint("R&D", -80.0),
            new WaterfallFinancialPoint("Admin", -40.0),
            new WaterfallFinancialPoint("Net Income", 130.0)
        };

        // Heikin-Ashi Chart (40 points)
        HeikinAshiData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(40));

        // Point and Figure Chart (100 points)
        PointAndFigureData = new ObservableCollection<FinancialPoint>(GenerateFinancialData(100));

        // Kagi Chart
        var kagiPoints = new List<KagiPoint>();
        double kagiPrice = 100;
        var r = new Random(123);
        for(int i=0; i<100; i++)
        {
            kagiPrice += (r.NextDouble() - 0.5) * 4; // Volatile walk
            kagiPoints.Add(new KagiPoint(kagiPrice));
        }
        KagiData = new ObservableCollection<KagiPoint>(kagiPoints);
    }

    private IEnumerable<FinancialPoint> GenerateFinancialData(int count, int seed = 42)
    {
        var result = new List<FinancialPoint>();
        var date = DateTime.Today.AddDays(-count);
        var price = 100.0;
        var rand = new Random(seed);

        for (int i = 0; i < count; i++)
        {
            var open = price;
            var close = open + (rand.NextDouble() - 0.5) * 5;
            var high = Math.Max(open, close) + rand.NextDouble() * 2;
            var low = Math.Min(open, close) - rand.NextDouble() * 2;

            var brush = close >= open
                ? new SolidColorBrush(Color.Parse("#16A34A"))
                : new SolidColorBrush(Color.Parse("#DC2626"));
            result.Add(new FinancialPoint(date.AddDays(i), open, high, low, close) { Brush = brush });
            price = close;
        }
        return result;
    }

    [ObservableProperty] private ObservableCollection<FinancialPoint> _candlestickData = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _styledCandlestickData = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _ohlcData = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _styledOhlcData = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _hiloData = null!;
    [ObservableProperty] private ObservableCollection<RenkoPoint> _renkoData = null!;
    [ObservableProperty] private ObservableCollection<WaterfallFinancialPoint> _waterfallData = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _heikinAshiData = null!;
    [ObservableProperty] private ObservableCollection<FinancialPoint> _pointAndFigureData = null!;
    [ObservableProperty] private ObservableCollection<KagiPoint> _kagiData = null!;
}

public record FinancialPoint(DateTime Date, double Open, double High, double Low, double Close)
{
    public IBrush? Brush { get; init; }
}
public record RenkoPoint(double Value);
public record KagiPoint(double Value);
public record WaterfallFinancialPoint(string Category, double Value);
