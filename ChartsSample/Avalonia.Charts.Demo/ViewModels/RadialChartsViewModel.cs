using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class RadialChartsViewModel : ViewModelBase
{
    public RadialChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        GeneratePolarData();

        // Radial Bar Chart
        RadialBarData = new ObservableCollection<RadialPoint>
        {
            new RadialPoint("Speed", 85.0),
            new RadialPoint("Power", 70.0),
            new RadialPoint("Agility", 60.0),
            new RadialPoint("Defense", 75.0),
            new RadialPoint("Stamina", 90.0)
        };

        // Polar Area Chart
        PolarChartData = new ObservableCollection<RadialPoint>
        {
            new RadialPoint("Speed", 85.0),
            new RadialPoint("Strength", 70.0),
            new RadialPoint("Agility", 60.0),
            new RadialPoint("Intellect", 75.0),
            new RadialPoint("Stamina", 90.0),
            new RadialPoint("Spirit", 65.0)
        };

        // Radar Chart
        RadarSeries1 = new ObservableCollection<double> { 80.0, 90.0, 70.0, 60.0, 85.0, 75.0 };
        RadarSeries2 = new ObservableCollection<double> { 60.0, 70.0, 85.0, 80.0, 65.0, 90.0 };
        RadarLabels = new ObservableCollection<string> { "Speed", "Power", "Agility", "Defense", "Stamina", "Technique" };

        // Nightingale Rose Chart
        NightingaleData = new ObservableCollection<RadialPoint>
        {
            new RadialPoint("Jan", 120.0),
            new RadialPoint("Feb", 180.0),
            new RadialPoint("Mar", 160.0),
            new RadialPoint("Apr", 200.0),
            new RadialPoint("May", 280.0),
            new RadialPoint("Jun", 350.0),
            new RadialPoint("Jul", 380.0),
            new RadialPoint("Aug", 340.0),
            new RadialPoint("Sep", 250.0),
            new RadialPoint("Oct", 180.0),
            new RadialPoint("Nov", 140.0),
            new RadialPoint("Dec", 160.0)
        };
    }

    [ObservableProperty] private ObservableCollection<RadialPoint> _radialBarData = null!;
    [ObservableProperty] private ObservableCollection<RadialPoint> _polarChartData = null!;
    [ObservableProperty] private ObservableCollection<double> _radarSeries1 = null!;
    [ObservableProperty] private ObservableCollection<double> _radarSeries2 = null!;
    [ObservableProperty] private ObservableCollection<string> _radarLabels = null!;
    [ObservableProperty] private ObservableCollection<RadialPoint> _nightingaleData = null!;
    
    [ObservableProperty] private ObservableCollection<PolarPoint> _spiralData = new();
    [ObservableProperty] private ObservableCollection<PolarPoint> _roseData = new();
    [ObservableProperty] private ObservableCollection<PolarPoint> _cardioidData = new();

    private void GeneratePolarData()
    {
        // Archimedean Spiral: r = a + b * theta
        var spiral = new ObservableCollection<PolarPoint>();
        for (double theta = 0; theta <= 720; theta += 5)
        {
            double r = 10 + 0.5 * theta;
            spiral.Add(new PolarPoint { Angle = theta, Radius = r });
        }
        SpiralData = spiral;

        // Rose Curve: r = 50 + 50 * cos(4 * theta)
        var rose = new ObservableCollection<PolarPoint>();
        for (double theta = 0; theta <= 360; theta += 2)
        {
            double r = 50 + 50 * System.Math.Cos(4 * theta * System.Math.PI / 180.0);
            rose.Add(new PolarPoint { Angle = theta, Radius = r });
        }
        RoseData = rose;
        
        // Cardioid: r = 50 * (1 - sin(theta))
        var cardioid = new ObservableCollection<PolarPoint>();
        for (double theta = 0; theta <= 360; theta += 5)
        {
            double r = 50 * (1 - System.Math.Sin(theta * System.Math.PI / 180.0));
            cardioid.Add(new PolarPoint { Angle = theta, Radius = r });
        }
        CardioidData = cardioid;
    }

    public class PolarPoint
    {
        public double Angle { get; set; }
        public double Radius { get; set; }
    }
    }

    public record RadialPoint(string Label, double Value)
    {
    // Alias property for different property path requirements if needed, 
    // or just use one standard name across charts if possible.
    // RadialBar uses "Category", Polar uses "Label".
    public string Category => Label; 
}
