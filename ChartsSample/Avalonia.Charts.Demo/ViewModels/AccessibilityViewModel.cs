using System.Collections.ObjectModel;
using Avalonia.Controls.Charts;
using Avalonia.Media;
using Avalonia.Charts.Demo.Models;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class AccessibilityViewModel : ViewModelBase
{
    private ChartPalette _currentPalette = default!;

    public ObservableCollection<ChartDataItem> PieData { get; }
    public ObservableCollection<ChartDataItem> BarSeriesQ1 { get; }
    public ObservableCollection<ChartDataItem> BarSeriesQ2 { get; }
    public ObservableCollection<ChartDataItem> BarSeriesQ3 { get; }

    public ChartPalette CurrentPalette
    {
        get => _currentPalette;
        set
        {
            if (_currentPalette != value)
            {
                _currentPalette = value;
                OnPropertyChanged();
            }
        }
    }

    public AccessibilityViewModel()
    {
        PieData = new ObservableCollection<ChartDataItem>
        {
            new ChartDataItem { Category = "Chrome", Value = 60 },
            new ChartDataItem { Category = "Safari", Value = 25 },
            new ChartDataItem { Category = "Firefox", Value = 10 },
            new ChartDataItem { Category = "Edge", Value = 5 },
        };

        BarSeriesQ1 = new ObservableCollection<ChartDataItem>
        {
            new ChartDataItem { Category = "A", Value = 10 },
            new ChartDataItem { Category = "B", Value = 20 },
            new ChartDataItem { Category = "C", Value = 30 },
        };

        BarSeriesQ2 = new ObservableCollection<ChartDataItem>
        {
            new ChartDataItem { Category = "A", Value = 15 },
            new ChartDataItem { Category = "B", Value = 25 },
            new ChartDataItem { Category = "C", Value = 35 },
        };

        BarSeriesQ3 = new ObservableCollection<ChartDataItem>
        {
            new ChartDataItem { Category = "A", Value = 12 },
            new ChartDataItem { Category = "B", Value = 22 },
            new ChartDataItem { Category = "C", Value = 32 },
        };

        // Initialize with the accessible palette
        CurrentPalette = ChartPatternPalette.CreateAccessibilityPalette();
    }

    public void ApplyAccessibilityPatterns()
    {
        CurrentPalette = ChartPatternPalette.CreateAccessibilityPalette();
    }

    public void ApplyHighContrast()
    {
        CurrentPalette = ChartPatternPalette.CreateHighContrastPalette();
    }

    public void ApplyStripesOnly()
    {
        var p = new ChartPatternPalette();
        p.Patterns = new System.Collections.Generic.List<PatternStyle>
        {
            PatternStyle.DiagonalStripe,
            PatternStyle.VerticalStripe,
            PatternStyle.HorizontalStripe
        };
        CurrentPalette = p;
    }

    public void ApplyDotsOnly()
    {
        var p = new ChartPatternPalette();
        p.Patterns = new System.Collections.Generic.List<PatternStyle>
        {
            PatternStyle.Dot
        };
        CurrentPalette = p;
    }

    public void ApplyBlackAndWhite()
    {
        var p = new ChartPatternPalette();
        p.BaseColors = new System.Collections.Generic.List<Color> { Colors.Black };
        p.Patterns = new System.Collections.Generic.List<PatternStyle>
        {
            PatternStyle.DiagonalStripe,
            PatternStyle.VerticalStripe,
            PatternStyle.HorizontalStripe,
            PatternStyle.Dot,
            PatternStyle.Crosshatch
        };
        CurrentPalette = p;
    }
}
