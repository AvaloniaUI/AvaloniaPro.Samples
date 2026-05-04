using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Applications;

public partial class CryptoDashboardViewModel : PageViewModel
{
    [ObservableProperty] private double _totalBalance = 9637.00;
    [ObservableProperty] private double _balanceChange = 175.00;
    [ObservableProperty] private double _balanceChangePercentage = 4.6;
    [ObservableProperty] private string _selectedPeriod = "1M";
    [ObservableProperty] private string _searchText = "";
    [ObservableProperty] private string _currentCurrencySymbol = "$";
    
    private string _selectedCurrency = "USD";
    public string SelectedCurrency
    {
        get => _selectedCurrency;
        set
        {
            if (SetProperty(ref _selectedCurrency, value))
            {
                UpdateCurrencySymbol(value);
                UpdateValuesForCurrency(value);
            }
        }
    }

    public CryptoDashboardViewModel() : base("Crypto Dashboard")
    {
        // Time periods for BTC chart
        TimePeriods = new ObservableCollection<string> { "1D", "3D", "5D", "1W", "2W", "1M", "3M", "6M", "1Y" };
        
        // Currencies for dropdown
        Currencies = new ObservableCollection<string> { "USD", "EUR", "GBP", "BTC" };

        // Calendar data for trade activity (28 days)
        CalendarData = new ObservableCollection<CalendarActivity>();
        var calendarValues = new[] { 3, 0, 2, 1, 2, 1, 3, 2, 2, 1, 1, 2, 0, 1, 2, 2, 3, 1, 2, 3, 2, 3, 2, 0, 3, 1, 3, 2 };
        for (int i = 0; i < 28; i++)
        {
            CalendarData.Add(new CalendarActivity(i + 1, calendarValues[i]));
        }

        // Wallet data - base values in USD
        WalletItems = new ObservableCollection<WalletItem>
        {
            new("BTC", "Bitcoin", 4.485, 235305, 4.6, "#2D9A8F", true),
            new("ETH", "Ethereum", 11.20, 34305, 3.8, "#627EEA", false),
            new("XRP", "XRP", 22934, 11864, -5.6, "#23292F", false),
            new("LTC", "Litecoin", 448.5, 19485, -5.6, "#345D9D", true)
        };

        // Base USD values for balance
        _baseTotalBalance = 9637.00;
        _baseBalanceChange = 175.00;

        // Trend data for area chart (more points and volatility)
        TrendData = new ObservableCollection<TrendPoint>
        {
            new(8800, 1), new(8950, 2), new(8820, 3), new(9100, 4), new(8950, 5),
            new(9200, 6), new(9380, 7), new(9100, 8), new(9450, 9), new(9300, 10),
            new(9550, 11), new(9400, 12), new(9650, 13), new(9500, 14), new(9637, 15),
            new(9450, 16), new(9680, 17), new(9550, 18), new(9750, 19), new(9637, 20)
        };

        // BTC OHLC data for candlestick chart (more "up and down" variation)
        BtcData = new ObservableCollection<BtcOhlc>();
        var random = new System.Random(42);
        var basePrice = 44000.0;
        for (int i = 0; i < 40; i++)
        {
            var open = basePrice;
            var close = basePrice + random.Next(-1500, 1800);
            var high = Math.Max(open, close) + random.Next(100, 800);
            var low = Math.Min(open, close) - random.Next(100, 800);
            BtcData.Add(new BtcOhlc(DateTime.Today.AddDays(-40 + i), open, high, low, close));
            basePrice = close;
        }
        // Force the last point to match mockup peak
        BtcData[^1] = new BtcOhlc(DateTime.Today, basePrice, 52430, basePrice - 500, 52430);
    }

    private readonly double _baseTotalBalance;
    private readonly double _baseBalanceChange;

    private void UpdateCurrencySymbol(string currency)
    {
        CurrentCurrencySymbol = currency switch
        {
            "USD" => "$",
            "EUR" => "€",
            "GBP" => "£",
            "BTC" => "₿",
            _ => "$"
        };
    }

    private void UpdateValuesForCurrency(string currency)
    {
        double rate = currency switch
        {
            "USD" => 1.0,
            "EUR" => 0.92,
            "GBP" => 0.79,
            "BTC" => 0.000015,
            _ => 1.0
        };

        TotalBalance = _baseTotalBalance * rate;
        BalanceChange = _baseBalanceChange * rate;

        // Update wallet balances
        var baseWalletValues = new[]
        {
            235305.0,
            34305.0,
            11864.0,
            19485.0
        };

        for (int i = 0; i < WalletItems.Count && i < baseWalletValues.Length; i++)
        {
            WalletItems[i].Balance = baseWalletValues[i] * rate;
        }
    }

    public ObservableCollection<string> TimePeriods { get; }
    public ObservableCollection<string> Currencies { get; }
    public ObservableCollection<CalendarActivity> CalendarData { get; }
    public ObservableCollection<WalletItem> WalletItems { get; }
    public ObservableCollection<TrendPoint> TrendData { get; }
    public ObservableCollection<BtcOhlc> BtcData { get; }
}

public record BtcOhlc(DateTime Date, double Open, double High, double Low, double Close);
public record CalendarActivity(int Day, int Count);
public record TrendPoint(double Value, int Index);

public partial class WalletItem : ObservableObject
{
    public string Symbol { get; }
    public string Name { get; }
    public double Amount { get; }
    [ObservableProperty] private double _balance;
    public double ChangePercentage { get; }
    public string Color { get; }
    public bool IsDark { get; }

    public WalletItem(string symbol, string name, double amount, double balance, double changePercentage, string color, bool isDark)
    {
        Symbol = symbol;
        Name = name;
        Amount = amount;
        Balance = balance;
        ChangePercentage = changePercentage;
        Color = color;
        IsDark = isDark;
    }
}
