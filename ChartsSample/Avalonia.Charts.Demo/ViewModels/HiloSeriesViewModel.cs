using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class HiloSeriesViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<FinancialDataPoint> _data;

    public HiloSeriesViewModel()
    {
        _data = new ObservableCollection<FinancialDataPoint>
        {
            new FinancialDataPoint { Date = new DateTime(2023, 1, 1), High = 120, Low = 100 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 2), High = 130, Low = 105 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 3), High = 125, Low = 95 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 4), High = 135, Low = 110 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 5), High = 140, Low = 115 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 6), High = 132, Low = 108 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 7), High = 145, Low = 120 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 8), High = 150, Low = 125 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 9), High = 142, Low = 118 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 10), High = 155, Low = 130 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 11), High = 160, Low = 135 },
            new FinancialDataPoint { Date = new DateTime(2023, 1, 12), High = 148, Low = 122 }
        };
    }
}

public class FinancialDataPoint
{
    public DateTime Date { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Open { get; set; }
    public double Close { get; set; }
}
