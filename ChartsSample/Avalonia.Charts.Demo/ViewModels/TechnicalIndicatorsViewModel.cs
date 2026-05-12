using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class TechnicalIndicatorsViewModel : PageViewModel
{
    public TechnicalIndicatorsViewModel() : base("Technical Indicators")
    {
        var random = new Random(1337);
        var price = 100.0;
        var trend = 1000.0;
        
        // Generate some realistic-looking candle/price data
        for (int i = 0; i < 100; i++)
        {
            price += (random.NextDouble() - 0.5) * 5;
            var open = price - 1;
            var high = price + random.NextDouble() * 3;
            var low = price - random.NextDouble() * 3;
            var close = price;

            StockData.Add(new FinancialPoint(DateTime.Now.AddDays(i), open, high, low, close));
            
            trend += (random.NextDouble() - 0.5) * 50;
             LargeData.Add(new { Category = i, Value = trend });
        }
    }

    public ObservableCollection<FinancialPoint> StockData { get; } = new();
    public ObservableCollection<object> LargeData { get; } = new();
}
