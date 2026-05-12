using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class ZoomingViewModel : ViewModelBase
{
    public ObservableCollection<DateTimePoint> ZoomData { get; private set; }
    public ObservableCollection<DateTimePoint> VolatileData { get; private set; }
    public ObservableCollection<ProductSales> ProductData { get; private set; }
    public ObservableCollection<ContinuousDatePoint> IrregularDateTimeZoomData { get; private set; }

    private double _standaloneRangeStart = 18;
    public double StandaloneRangeStart
    {
        get => _standaloneRangeStart;
        set
        {
            if (SetProperty(ref _standaloneRangeStart, value))
            {
                OnPropertyChanged(nameof(StandaloneRangeSummary));
            }
        }
    }

    private double _standaloneRangeEnd = 74;
    public double StandaloneRangeEnd
    {
        get => _standaloneRangeEnd;
        set
        {
            if (SetProperty(ref _standaloneRangeEnd, value))
            {
                OnPropertyChanged(nameof(StandaloneRangeSummary));
            }
        }
    }

    public string StandaloneRangeSummary => $"{StandaloneRangeStart:0} - {StandaloneRangeEnd:0}";

    private double _standaloneVerticalRangeStart = 12;
    public double StandaloneVerticalRangeStart
    {
        get => _standaloneVerticalRangeStart;
        set
        {
            if (SetProperty(ref _standaloneVerticalRangeStart, value))
            {
                OnPropertyChanged(nameof(StandaloneVerticalRangeSummary));
            }
        }
    }

    private double _standaloneVerticalRangeEnd = 86;
    public double StandaloneVerticalRangeEnd
    {
        get => _standaloneVerticalRangeEnd;
        set
        {
            if (SetProperty(ref _standaloneVerticalRangeEnd, value))
            {
                OnPropertyChanged(nameof(StandaloneVerticalRangeSummary));
            }
        }
    }

    public string StandaloneVerticalRangeSummary => $"{StandaloneVerticalRangeStart:0} - {StandaloneVerticalRangeEnd:0}";

    private double _zoomFactorX = 1.0;
    public double ZoomFactorX
    {
        get => _zoomFactorX;
        set => SetProperty(ref _zoomFactorX, value);
    }

    private double _zoomFactorY = 1.0;
    public double ZoomFactorY
    {
        get => _zoomFactorY;
        set => SetProperty(ref _zoomFactorY, value);
    }

    private string _zoomInfo = "No zoom applied";
    public string ZoomInfo
    {
        get => _zoomInfo;
        set => SetProperty(ref _zoomInfo, value);
    }

    private bool _isPanBoundsEnabled = true;
    public bool IsPanBoundsEnabled
    {
        get => _isPanBoundsEnabled;
        set => SetProperty(ref _isPanBoundsEnabled, value);
    }



    private double _zoomSensitivity = 0.1;
    public double ZoomSensitivity
    {
        get => _zoomSensitivity;
        set => SetProperty(ref _zoomSensitivity, value);
    }

    public ZoomingViewModel()
    {
        ZoomData = GenerateData();
        VolatileData = GenerateVolatileData();
        ProductData = GenerateProductData();
        IrregularDateTimeZoomData = GenerateIrregularDateTimeZoomData();
    }

    private ObservableCollection<DateTimePoint> GenerateData()
    {
        var data = new ObservableCollection<DateTimePoint>();
        var date = new DateTime(2023, 1, 1);
        var rand = new Random(42);
        double value = 100;

        // Generate 365 points (1 year of daily data)
        for (int i = 0; i < 365; i++)
        {
            // Random walk with trend
            value += rand.NextDouble() * 10 - 4.5; // Slight upward trend
            value = Math.Max(50, Math.Min(200, value)); // Keep within bounds
            data.Add(new DateTimePoint { Date = date, Value = value });
            date = date.AddDays(1);
        }

        return data;
    }

    private ObservableCollection<DateTimePoint> GenerateVolatileData()
    {
        var data = new ObservableCollection<DateTimePoint>();
        var date = new DateTime(2023, 1, 1);
        var rand = new Random(123);
        double value = 50;

        // Generate highly volatile data for demonstrating zoom constraints
        for (int i = 0; i < 200; i++)
        {
            value += rand.NextDouble() * 30 - 15;
            value = Math.Max(0, Math.Min(100, value));
            data.Add(new DateTimePoint { Date = date, Value = value });
            date = date.AddDays(1);
        }

        return data;
    }

    private ObservableCollection<ProductSales> GenerateProductData()
    {
        return new ObservableCollection<ProductSales>
        {
            new() { Product = "Electronics", Sales = 15000 },
            new() { Product = "Clothing", Sales = 12000 },
            new() { Product = "Food", Sales = 18000 },
            new() { Product = "Books", Sales = 8000 },
            new() { Product = "Sports", Sales = 11000 },
            new() { Product = "Home", Sales = 9500 },
            new() { Product = "Beauty", Sales = 7200 },
            new() { Product = "Toys", Sales = 6800 },
            new() { Product = "Garden", Sales = 5400 },
            new() { Product = "Auto", Sales = 14200 }
        };
    }

    private ObservableCollection<ContinuousDatePoint> GenerateIrregularDateTimeZoomData()
    {
        var start = new DateTime(2026, 4, 1);

        return new ObservableCollection<ContinuousDatePoint>
        {
            new(start, 32),
            new(start.AddHours(8), 38),
            new(start.AddDays(1), 35),
            new(start.AddDays(3).AddHours(12), 49),
            new(start.AddDays(4), 44),
            new(start.AddDays(8), 61),
            new(start.AddDays(11), 58),
            new(start.AddDays(18), 72),
            new(start.AddDays(20), 68)
        };
    }
}

public class DateTimePoint
{
    public DateTime Date { get; set; }
    public double Value { get; set; }
}

public class ProductSales
{
    public string Product { get; set; } = string.Empty;
    public double Sales { get; set; }
}
