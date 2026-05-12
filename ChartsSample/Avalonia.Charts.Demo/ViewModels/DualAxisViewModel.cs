using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class DualAxisViewModel : ViewModelBase
{
    public ObservableCollection<RevenueGrowthItem> RevenueGrowthData { get; }

    public DualAxisViewModel()
    {
        RevenueGrowthData = new ObservableCollection<RevenueGrowthItem>
        {
            new() { Year = "2018", Revenue = 120, Growth = 12 },
            new() { Year = "2019", Revenue = 150, Growth = 25 },
            new() { Year = "2020", Revenue = 180, Growth = 20 },
            new() { Year = "2021", Revenue = 220, Growth = 22 },
            new() { Year = "2022", Revenue = 280, Growth = 27 },
            new() { Year = "2023", Revenue = 350, Growth = 25 }
        };

        RainfallFlowData = new ObservableCollection<RainfallFlowItem>();
        GenerateRainfallData();
    }

    public ObservableCollection<RainfallFlowItem> RainfallFlowData { get; }

    private void GenerateRainfallData()
    {
        // Sample data mimicking the relationship: high rain often leads to high flow
        var data = new[]
        {
            (Time: "10:00", Rain: 0.2, Flow: 2.5),
            (Time: "11:00", Rain: 0.5, Flow: 5.0),
            (Time: "12:00", Rain: 2.0, Flow: 8.0),
            (Time: "13:00", Rain: 8.5, Flow: 15.0),
            (Time: "14:00", Rain: 15.0, Flow: 35.0),
            (Time: "15:00", Rain: 12.0, Flow: 55.0),
            (Time: "16:00", Rain: 6.0, Flow: 75.0), // Peak flow lags peak rain
            (Time: "17:00", Rain: 2.0, Flow: 65.0),
            (Time: "18:00", Rain: 0.5, Flow: 45.0),
            (Time: "19:00", Rain: 0.1, Flow: 30.0),
            (Time: "20:00", Rain: 0.0, Flow: 20.0),
            (Time: "21:00", Rain: 0.0, Flow: 15.0),
            (Time: "22:00", Rain: 0.0, Flow: 12.0)
        };

        foreach (var item in data)
        {
            RainfallFlowData.Add(new RainfallFlowItem { Time = item.Time, Rainfall = item.Rain, Flow = item.Flow });
        }
    }
}

public class RevenueGrowthItem
{
    public string Year { get; set; } = string.Empty;
    public double Revenue { get; set; }
    public double Growth { get; set; }
}

public class RainfallFlowItem
{
    public string Time { get; set; } = string.Empty;
    public double Rainfall { get; set; }
    public double Flow { get; set; }
}
