using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Media;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class AppearanceChartsViewModel : ObservableObject
{
    [ObservableProperty]
    private ChartPalette _customPalette = null!;

    [ObservableProperty]
    private ObservableCollection<double> _gradientData = null!;

    [ObservableProperty]
    private ObservableCollection<double> _backgroundData = null!;

    [ObservableProperty]
    private ObservableCollection<double> _watermarkData = null!;

    [ObservableProperty]
    private ObservableCollection<FinancialPoint> _styledFinancialData = null!;

    [ObservableProperty]
    private ObservableCollection<ContourSamplePoint> _styledContourData = null!;

    [ObservableProperty]
    private ObservableCollection<PolarStylePoint> _styledPolarData = null!;

    public AppearanceChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Custom Gradient Palette
        var gradients = new IBrush[]
        {
            new LinearGradientBrush
            {
                StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                EndPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
                GradientStops =
                {
                    new GradientStop(Colors.DeepSkyBlue, 0),
                    new GradientStop(Colors.DodgerBlue, 1)
                }
            },
            new LinearGradientBrush
            {
                StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                EndPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
                GradientStops =
                {
                    new GradientStop(Colors.LimeGreen, 0),
                    new GradientStop(Colors.SeaGreen, 1)
                }
            },
            new LinearGradientBrush
            {
                StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                EndPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
                GradientStops =
                {
                    new GradientStop(Colors.Orange, 0),
                    new GradientStop(Colors.OrangeRed, 1)
                }
            },
            new LinearGradientBrush
            {
                StartPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
                EndPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
                GradientStops =
                {
                    new GradientStop(Colors.MediumPurple, 0),
                    new GradientStop(Colors.Purple, 1)
                }
            }
        };
        CustomPalette = ChartPalette.FromBrushes(gradients);
        GradientData = new ObservableCollection<double> { 150, 230, 180, 260 };

        // PlotArea Background
        BackgroundData = new ObservableCollection<double> { 85, 92, 78, 95, 88, 102 };

        // Watermark
        WatermarkData = new ObservableCollection<double> { 120, 145, 138, 165, 152, 178 };

        StyledFinancialData = new ObservableCollection<FinancialPoint>(CreateFinancialData());
        StyledContourData = new ObservableCollection<ContourSamplePoint>(CreateContourData());
        StyledPolarData = new ObservableCollection<PolarStylePoint>(CreatePolarData());
    }

    private static IEnumerable<FinancialPoint> CreateFinancialData()
    {
        var random = new Random(11);
        var date = DateTime.Today.AddDays(-18);
        var price = 86.0;

        for (var i = 0; i < 18; i++)
        {
            var open = price;
            var close = open + (random.NextDouble() - 0.45) * 5;
            var high = Math.Max(open, close) + random.NextDouble() * 2;
            var low = Math.Min(open, close) - random.NextDouble() * 2;
            yield return new FinancialPoint(date.AddDays(i), open, high, low, close);
            price = close;
        }
    }

    private static IEnumerable<ContourSamplePoint> CreateContourData()
    {
        for (var x = -4; x <= 4; x++)
        {
            for (var y = -4; y <= 4; y++)
            {
                var value = Math.Sin(x * 0.8) * Math.Cos(y * 0.8) * 10 + x + y;
                yield return new ContourSamplePoint(x, y, value);
            }
        }
    }

    private static IEnumerable<PolarStylePoint> CreatePolarData()
    {
        for (var angle = 0; angle <= 360; angle += 12)
        {
            var radians = angle * Math.PI / 180.0;
            var radius = 45 + 18 * Math.Sin(radians * 3) + 8 * Math.Cos(radians * 5);
            yield return new PolarStylePoint(angle, radius);
        }
    }
}

public record ContourSamplePoint(double X, double Y, double Z);

public record PolarStylePoint(double Angle, double Radius);
