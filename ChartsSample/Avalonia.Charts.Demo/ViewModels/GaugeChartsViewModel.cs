using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class GaugeChartsViewModel : ViewModelBase
{
    public ObservableCollection<RingItem> RingData { get; }

    public GaugeChartsViewModel()
    {
        RingData = new ObservableCollection<RingItem>
        {
            new() { Name = "Design", Progress = 85.0 },
            new() { Name = "Backend", Progress = 72.0 },
            new() { Name = "Frontend", Progress = 68.0 },
            new() { Name = "Testing", Progress = 45.0 },
            new() { Name = "Deploy", Progress = 30.0 },
        };
    }
}

public class RingItem
{
    public string Name { get; set; } = string.Empty;
    public double Progress { get; set; }
}
