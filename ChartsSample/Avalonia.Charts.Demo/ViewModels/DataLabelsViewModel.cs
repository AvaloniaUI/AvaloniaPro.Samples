using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class DataLabelsViewModel : ViewModelBase
{
    public ObservableCollection<ChartItem> SalesData { get; }
    public ObservableCollection<ChartItem> ProfitData { get; }

    public DataLabelsViewModel()
    {
        SalesData = new ObservableCollection<ChartItem>
        {
            new() { Category = "Q1", Value = 120 },
            new() { Category = "Q2", Value = 150 },
            new() { Category = "Q3", Value = 180 },
            new() { Category = "Q4", Value = 220 }
        };

        ProfitData = new ObservableCollection<ChartItem>
        {
            new() { Category = "Jan", Value = 5000 },
            new() { Category = "Feb", Value = 7500 },
            new() { Category = "Mar", Value = 6200 },
            new() { Category = "Apr", Value = 8900 }
        };
    }
}

public class ChartItem
{
    public string Category { get; set; } = string.Empty;
    public double Value { get; set; }
    public string Label { get; set; } = string.Empty;
    public string AgeLabel { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
