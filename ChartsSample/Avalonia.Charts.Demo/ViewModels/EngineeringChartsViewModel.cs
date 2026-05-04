using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class EngineeringChartsViewModel : ViewModelBase
{
    public EngineeringChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Carpet Plot
        var carpetList = GenerateAerodynamicData();
        CarpetData = new ObservableCollection<dynamic>(carpetList);

        // Ternary Chart
        TernaryData = new ObservableCollection<dynamic>
        {
            new { A = 0.6, B = 0.2, C = 0.2 },
            new { A = 0.3, B = 0.4, C = 0.3 },
            new { A = 0.2, B = 0.5, C = 0.3 },
            new { A = 0.4, B = 0.3, C = 0.3 },
            new { A = 0.33, B = 0.33, C = 0.34 },
        };

        // Wind Rose
        var windData = new List<dynamic>();
        var directions = new[] { "N", "NE", "E", "SE", "S", "SW", "W", "NW" };
        var speeds = new[] { "0-5 m/s", "5-10 m/s", "10+ m/s" };
        var r = new Random(42);

        foreach (var d in directions)
        {
             // Predominant wind from W and SW
            double factor = (d == "W" || d == "SW") ? 2.0 : 1.0;
            
            foreach (var s in speeds)
            {
                // Speed decay
                double speedFactor = s == "0-5 m/s" ? 1.0 : (s == "5-10 m/s" ? 0.6 : 0.3);
                windData.Add(new { Dir = d, Speed = s, Freq = r.NextDouble() * 20 * factor * speedFactor + 5 });
            }
        }
        WindRoseData = new ObservableCollection<dynamic>(windData);

        // Smith Chart
        var smithPoints = new List<dynamic>();
        // Simulate antenna resonance around 100 MHz (Normalized Z0=50)
        for (double f = 80; f <= 120; f += 1)
        {
            double f0 = 100;
            double Q = 2; // Low Q for wider curve
            
            // Normalized Frequency deviation
            double delta = (f / f0) - (f0 / f);
            
            double res = 1.0; // Matched at resonance (Normalized R=1) (renamed to res to avoid conflict)
            double x = Q * delta;
            
            smithPoints.Add(new { F = f, R = res, X = x });
        }
        SmithChartData = new ObservableCollection<dynamic>(smithPoints);
    }

    private IEnumerable<dynamic> GenerateAerodynamicData()
    {
        var data = new List<dynamic>();
        
        // Mach numbers (0.2 to 0.8)
        var machs = new[] { 0.2, 0.4, 0.6, 0.8 };
        
        // Angles of Attack (-4 to 12 degrees)
        var alphas = new[] { -4.0, 0.0, 4.0, 8.0, 12.0 };

        foreach (var m in machs)
        {
            foreach (var alpha in alphas)
            {
                // Fake Lift Coefficient formula: Cl = 2 * pi * alpha_rad * (1 / sqrt(1 - M^2))
                var alphaRad = (alpha + 5) * Math.PI / 180.0; // Shifted so -4 isn't negative lift
                var prandtlGlauert = 1.0 / Math.Sqrt(1 - m * m);
                var cl = 2 * Math.PI * alphaRad * prandtlGlauert;

                // Add some non-linearity for carpet effect
                cl += 0.05 * alpha * m;

                data.Add(new { Mach = m, Alpha = alpha, Cl = cl });
            }
        }
        
        return data;
    }

    [ObservableProperty] private ObservableCollection<dynamic> _carpetData = null!;
    [ObservableProperty] private ObservableCollection<dynamic> _ternaryData = null!;
    [ObservableProperty] private ObservableCollection<dynamic> _windRoseData = null!;
    [ObservableProperty] private ObservableCollection<dynamic> _smithChartData = null!;
}
