using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class BubbleChartsViewModel : ViewModelBase
{
    public BubbleChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Bubble Chart
        BubbleSeriesData = new ObservableCollection<BubblePoint>
        {
            new BubblePoint(1, 15, 10, "A"),
            new BubblePoint(2, 25, 20, "B"),
            new BubblePoint(3, 35, 15, "C"),
            new BubblePoint(4, 20, 25, "D"),
            new BubblePoint(5, 45, 30, "E")
        };

        // Packed Bubble Chart
        PackedBubbleData = new ObservableCollection<BubbleValue>
        {
            new BubbleValue("Tech", 45.0),
            new BubbleValue("Finance", 30.0),
            new BubbleValue("Health", 25.0),
            new BubbleValue("Energy", 20.0),
            new BubbleValue("Retail", 15.0)
        };

        // Bubble Cloud
        BubbleCloudData = new ObservableCollection<BubbleValue>
        {
            new BubbleValue("React", 80.0),
            new BubbleValue("Vue", 60.0),
            new BubbleValue("Angular", 50.0),
            new BubbleValue("Svelte", 40.0),
            new BubbleValue("Nuxt", 35.0),
            new BubbleValue("Next", 30.0)
        };

        // Hexbin Chart
        var random = new Random(42);
        var points = new ObservableCollection<HexbinPoint>();
        for (int i = 0; i < 100; i++)
        {
            points.Add(new HexbinPoint(random.NextDouble() * 100, random.NextDouble() * 100));
        }
        HexbinData = points;
    }

    [ObservableProperty] private ObservableCollection<BubblePoint> _bubbleSeriesData = null!;
    [ObservableProperty] private ObservableCollection<BubbleValue> _packedBubbleData = null!;
    [ObservableProperty] private ObservableCollection<BubbleValue> _bubbleCloudData = null!;
    [ObservableProperty] private ObservableCollection<HexbinPoint> _hexbinData = null!;
}

public record BubblePoint(double X, double Y, double Size, string Label);
public record BubbleValue(string Name, double Value);
public record HexbinPoint(double X, double Y);
