using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class StatisticalChartsViewModel : ViewModelBase
{
    public StatisticalChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        var random = new Random(42);

        // Histogram
        HistogramData = new ObservableCollection<HistogramItem>(
            Enumerable.Range(0, 50).Select(_ => new HistogramItem(50 + random.NextDouble() * 50)));

        // Box Plot
        BoxPlotData = new ObservableCollection<BoxPlotItem>
        {
            new("Class A", 45.0, 55.0, 65.0, 75.0, 90.0),
            new("Class B", 50.0, 62.0, 72.0, 82.0, 95.0),
            new("Class C", 38.0, 48.0, 58.0, 68.0, 85.0)
        };

        // Error Bar
        ErrorBarData = new ObservableCollection<ErrorBarItem>
        {
            new("A", 45.0, 5.0),
            new("B", 62.0, 8.0),
            new("C", 38.0, 4.0),
            new("D", 75.0, 10.0),
            new("E", 55.0, 6.0)
        };

        // Confidence Interval
        ModelAData = new ObservableCollection<ConfidenceItem>
        {
            new("30min", 0.12, 0.08, 0.18),
            new("2h", 0.28, 0.20, 0.36),
            new("8h", 0.65, 0.52, 0.78),
            new("16h", 0.91, 0.75, 1.07),
            new("32h", 1.14, 0.92, 1.36)
        };
        ModelBData = new ObservableCollection<ConfidenceItem>
        {
            new("30min", 0.08, 0.04, 0.14),
            new("2h", 0.18, 0.12, 0.26),
            new("8h", 0.28, 0.20, 0.38),
            new("16h", 0.35, 0.25, 0.48),
            new("32h", 0.45, 0.32, 0.62)
        };
        ModelCData = new ObservableCollection<ConfidenceItem>
        {
            new("30min", 0.05, 0.02, 0.10),
            new("2h", 0.10, 0.05, 0.18),
            new("8h", 0.22, 0.14, 0.32),
            new("16h", 0.32, 0.22, 0.46),
            new("32h", 0.42, 0.28, 0.58)
        };

        // Diverging Bar
        DivergingData = new ObservableCollection<DivergingItem>
        {
            new("Product Quality", 25.0),
            new("Customer Service", 15.0),
            new("Price Value", -8.0),
            new("Delivery Speed", 32.0),
            new("Website UX", -12.0),
            new("Return Policy", 18.0),
            new("Mobile App", -5.0)
        };

        // Violin Plot
        ViolinData = new ObservableCollection<ViolinItem>
        {
            new("Group A", Enumerable.Range(0, 100).Select(_ => random.NextDouble() * 10 + 5).ToArray()),
            new("Group B", Enumerable.Range(0, 100).Select(_ => random.NextDouble() * 15 + 10).ToArray()),
            new("Group C", Enumerable.Range(0, 100).Select(_ => random.NextDouble() * 8 + 2).ToArray())
        };

        // Pareto
        ParetoData = new ObservableCollection<ParetoItem>
        {
            new("Missing Parts", 45),
            new("Surface Damage", 30),
            new("Wrong Size", 20),
            new("Color Error", 15),
            new("Other", 10)
        };

        // Slope
        SlopeData = new ObservableCollection<SlopeItem>
        {
            new("Sales", 100.0, 150.0),
            new("Cost", 80.0, 70.0),
            new("Profit", 20.0, 80.0)
        };

        // Dumbbell
        DumbbellData = new ObservableCollection<DumbbellItem>
        {
            new("Sales", 100.0, 150.0),
            new("Marketing", 80.0, 120.0),
            new("Support", 60.0, 85.0),
            new("Dev", 120.0, 180.0)
        };

        // Mirror Bar
        MirrorData = new ObservableCollection<MirrorItem>
        {
            new("18-24", 45.0, 42.0),
            new("25-34", 60.0, 58.0),
            new("35-44", 55.0, 52.0),
            new("45-54", 40.0, 45.0),
            new("55+", 30.0, 35.0)
        };

        // Tornado
        TornadoData = new ObservableCollection<TornadoItem>
        {
            new("Market Size", 70.0, 140.0),
            new("Unit Cost", 85.0, 120.0),
            new("Adoption Rate", 80.0, 130.0),
            new("Price", 90.0, 115.0)
        };

        // Bump Chart
        BumpData = new ObservableCollection<BumpItem>
        {
            new("Java", new[] { 1, 2, 2, 3, 3 }),
            new("C#", new[] { 4, 3, 3, 2, 2 }),
            new("Python", new[] { 2, 1, 1, 1, 1 }),
            new("JS", new[] { 3, 4, 4, 4, 4 })
        };
        
        BumpPeriods = new string[] { "2020", "2021", "2022", "2023", "2024" };

        // Contour Plot
        var points = new List<ContourPoint>();
        for (double x = -10; x <= 10; x += 1)
        {
            for (double y = -10; y <= 10; y += 1)
            {
                double z = 3 * Math.Pow(1 - x / 5, 2) * Math.Exp(-(x / 5) * (x / 5) - (y / 5 + 1) * (y / 5 + 1))
                           - 10 * (x / 5 / 5 - Math.Pow(x / 5, 3) - Math.Pow(y / 5, 5)) * Math.Exp(-(x / 5) * (x / 5) - (y / 5) * (y / 5))
                           - 1.0 / 3 * Math.Exp(-(x / 5 + 1) * (x / 5 + 1) - (y / 5) * (y / 5));
                points.Add(new ContourPoint(x, y, z));
            }
        }
        ContourData = new ObservableCollection<ContourPoint>(points);

        // Density Plot
        var ages = new List<double>();
        for (int i = 0; i < 80; i++) ages.Add(20 + random.NextDouble() * 15);
        for (int i = 0; i < 60; i++) ages.Add(38 + random.NextDouble() * 18);
        for (int i = 0; i < 20; i++) ages.Add(55 + random.NextDouble() * 25);
        DensityData = new ObservableCollection<double>(ages);

        // Beeswarm
        var beeswarmData = new List<BeeswarmItem>();
        foreach (var group in new[] { "Control", "Treatment A", "Treatment B" })
        {
            for (int i = 0; i < 30; i++)
            {
                var baseValue = group == "Control" ? 50 : (group == "Treatment A" ? 65 : 75);
                beeswarmData.Add(new BeeswarmItem(group, baseValue + random.NextDouble() * 20 - 10));
            }
        }
        BeeswarmData = new ObservableCollection<BeeswarmItem>(beeswarmData);

        // Strip Plot
        var stripData = new List<StripItem>();
        foreach (var group in new[] { "Fast", "Medium", "Slow" })
        {
            var count = group == "Medium" ? 50 : 25;
            for (int i = 0; i < count; i++)
            {
                var baseValue = group == "Fast" ? 100 : (group == "Medium" ? 300 : 600);
                stripData.Add(new StripItem(group, baseValue + random.NextDouble() * 150));
            }
        }
        StripData = new ObservableCollection<StripItem>(stripData);

        // Population Pyramid
        PopulationData = new ObservableCollection<PopulationItem>
        {
            new("0-9", 5.2, 4.9),
            new("10-19", 5.8, 5.5),
            new("20-29", 7.1, 6.8),
            new("30-39", 7.5, 7.2),
            new("40-49", 6.8, 6.9),
            new("50-59", 6.2, 6.5),
            new("60-69", 5.1, 5.8),
            new("70-79", 3.5, 4.2),
            new("80+", 1.8, 2.9)
        };

        // Ridgeline
        RidgelineData2020 = new ObservableCollection<RidgelineItem>(Enumerable.Range(0, 20).Select(i => new RidgelineItem(i, random.NextDouble() * 40 + 20)));
        RidgelineData2021 = new ObservableCollection<RidgelineItem>(Enumerable.Range(0, 20).Select(i => new RidgelineItem(i, random.NextDouble() * 50 + 30)));
        RidgelineData2022 = new ObservableCollection<RidgelineItem>(Enumerable.Range(0, 20).Select(i => new RidgelineItem(i, random.NextDouble() * 60 + 40)));

        // Mekko
        MekkoData = new ObservableCollection<MekkoItem>
        {
            new("North", 40.0, new[] { new MekkoSegment("Product A", 30.0), new MekkoSegment("Product B", 45.0) }),
            new("South", 35.0, new[] { new MekkoSegment("Product A", 25.0), new MekkoSegment("Product B", 35.0) })
        };

        // Parallel Coordinates
        ParallelData = new ObservableCollection<ParallelItem>
        {
            new(80.0, 60.0, 90.0),
            new(50.0, 90.0, 30.0),
            new(30.0, 40.0, 70.0)
        };

        // Multi-Slope (Spline)
        MultiSlopeBlueData = new ObservableCollection<MultiSlopePoint>
        {
            new("Category 1", 30), new("Category 2", 70), new("Category 3", 10)
        };
        MultiSlopeGreenData = new ObservableCollection<MultiSlopePoint>
        {
            new("Category 1", 90), new("Category 2", 30), new("Category 3", 85)
        };
        MultiSlopeRedData = new ObservableCollection<MultiSlopePoint>
        {
            new("Category 1", 20), new("Category 2", 85), new("Category 3", 60)
        };
        MultiSlopeOrangeData = new ObservableCollection<MultiSlopePoint>
        {
            new("Category 1", 10), new("Category 2", 10), new("Category 3", 40)
        };

        // Venn Diagram
        VennData = new ObservableCollection<VennItem>
        {
            new VennItem { Sets = new[] { "A" }, Value = 2, Name = "Good", Fill = new SolidColorBrush(Color.Parse("#93C5FD")) },
            new VennItem { Sets = new[] { "B" }, Value = 2, Name = "Cheap", Fill = new SolidColorBrush(Color.Parse("#FCD34D")) },
            new VennItem { Sets = new[] { "C" }, Value = 2, Name = "Fast", Fill = new SolidColorBrush(Color.Parse("#F87171")) },

            // Intersections
            new VennItem { Sets = new[] { "A", "B" }, Value = 1, Name = "Dream", Fill = new SolidColorBrush(Color.Parse("#A78BFA")) },
            new VennItem { Sets = new[] { "B", "C" }, Value = 1, Name = "Delivered", Fill = new SolidColorBrush(Color.Parse("#FDBA74")) },
            new VennItem { Sets = new[] { "A", "C" }, Value = 1, Name = "Expensive", Fill = new SolidColorBrush(Color.Parse("#34D399")) },
            new VennItem { Sets = new[] { "A", "B", "C" }, Value = 0.5, Name = "Utopia", Fill = new SolidColorBrush(Color.Parse("#60A5FA")) }
        };
    }

    [ObservableProperty] private ObservableCollection<HistogramItem> _histogramData = null!;
    [ObservableProperty] private ObservableCollection<BoxPlotItem> _boxPlotData = null!;
    [ObservableProperty] private ObservableCollection<ErrorBarItem> _errorBarData = null!;
    [ObservableProperty] private ObservableCollection<ConfidenceItem> _modelAData = null!;
    [ObservableProperty] private ObservableCollection<ConfidenceItem> _modelBData = null!;
    [ObservableProperty] private ObservableCollection<ConfidenceItem> _modelCData = null!;
    [ObservableProperty] private ObservableCollection<DivergingItem> _divergingData = null!;
    [ObservableProperty] private ObservableCollection<ViolinItem> _violinData = null!;
    [ObservableProperty] private ObservableCollection<ParetoItem> _paretoData = null!;
    [ObservableProperty] private ObservableCollection<SlopeItem> _slopeData = null!;
    [ObservableProperty] private ObservableCollection<DumbbellItem> _dumbbellData = null!;
    [ObservableProperty] private ObservableCollection<MirrorItem> _mirrorData = null!;
    [ObservableProperty] private ObservableCollection<TornadoItem> _tornadoData = null!;
    [ObservableProperty] private ObservableCollection<BumpItem> _bumpData = null!;
    [ObservableProperty] private IList<string> _bumpPeriods = null!;
    [ObservableProperty] private ObservableCollection<ContourPoint> _contourData = null!;
    [ObservableProperty] private ObservableCollection<double> _densityData = null!;
    [ObservableProperty] private ObservableCollection<BeeswarmItem> _beeswarmData = null!;
    [ObservableProperty] private ObservableCollection<StripItem> _stripData = null!;
    [ObservableProperty] private ObservableCollection<PopulationItem> _populationData = null!;
    [ObservableProperty] private ObservableCollection<RidgelineItem> _ridgelineData2020 = null!;
    [ObservableProperty] private ObservableCollection<RidgelineItem> _ridgelineData2021 = null!;
    [ObservableProperty] private ObservableCollection<RidgelineItem> _ridgelineData2022 = null!;
    [ObservableProperty] private ObservableCollection<MekkoItem> _mekkoData = null!;
    [ObservableProperty] private ObservableCollection<ParallelItem> _parallelData = null!;

    [ObservableProperty] private ObservableCollection<MultiSlopePoint> _multiSlopeBlueData = null!;
    [ObservableProperty] private ObservableCollection<MultiSlopePoint> _multiSlopeGreenData = null!;
    [ObservableProperty] private ObservableCollection<MultiSlopePoint> _multiSlopeRedData = null!;
    [ObservableProperty] private ObservableCollection<MultiSlopePoint> _multiSlopeOrangeData = null!;

    [ObservableProperty] private ObservableCollection<VennItem> _vennData = null!;
}

// Data Records
public record HistogramItem(double Score);
public record BoxPlotItem(string Class, double Min, double Q1, double Median, double Q3, double Max);
public record ErrorBarItem(string Sample, double Value, double Error);
public record ConfidenceItem(string Period, double Value, double Lower, double Upper);
public record DivergingItem(string Category, double Score);
public record ViolinItem(string Category, double[] Values);
public record ParetoItem(string Defect, int Count);
public record SlopeItem(string Label, double Before, double After);
public record DumbbellItem(string Category, double Before, double After);
public record MirrorItem(string Category, double Male, double Female);
public record TornadoItem(string Factor, double Low, double High);
public record BumpItem(string Name, int[] Ranks);
public record ContourPoint(double X, double Y, double Z);
public record BeeswarmItem(string Category, double Value);
public record StripItem(string Category, double Value);
public record PopulationItem(string AgeGroup, double Male, double Female);
public record RidgelineItem(double X, double Y);
public record MekkoItem(string Category, double Width, MekkoSegment[] Segments);
public record MekkoSegment(string Name, double Value);
public record ParallelItem(double Speed, double Power, double Efficiency);
public record MultiSlopePoint(string Category, double Value);
