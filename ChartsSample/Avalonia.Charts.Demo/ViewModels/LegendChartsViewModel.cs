using System.Collections.ObjectModel;
using Avalonia.Controls.Charts;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class LegendChartsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<int> _data1;

    [ObservableProperty]
    private ObservableCollection<int> _data2;

    [ObservableProperty]
    private ObservableCollection<int> _data3;

    [ObservableProperty]
    private bool _isLegendVisible = true;

    [ObservableProperty]
    private bool _isSeries1Visible = true;

    [ObservableProperty]
    private ObservableCollection<ParliamentParty> _parliamentParties;

    [ObservableProperty]
    private ObservableCollection<LegendMekkoItem> _mekkoData;

    [ObservableProperty]
    private ObservableCollection<ChartLegendItem> _standaloneLegendItems;

    public LegendChartsViewModel()
    {
        _data1 = new ObservableCollection<int> { 10, 20, 30, 40, 50 };
        _data2 = new ObservableCollection<int> { 15, 25, 35, 45, 55 };
        _data3 = new ObservableCollection<int> { 5, 15, 10, 20, 10 };
        _parliamentParties = new ObservableCollection<ParliamentParty>
        {
            new() { Name = "North", Seats = 42, Color = Color.Parse("#2563EB") },
            new() { Name = "South", Seats = 31, Color = Color.Parse("#16A34A") },
            new() { Name = "Central", Seats = 24, Color = Color.Parse("#F97316") },
            new() { Name = "Independent", Seats = 13, Color = Color.Parse("#64748B") }
        };
        _mekkoData = new ObservableCollection<LegendMekkoItem>
        {
            new("Enterprise", 45, [new("Cloud", 32), new("Desktop", 18), new("Services", 24)]),
            new("SMB", 28, [new("Cloud", 21), new("Desktop", 30), new("Services", 12)]),
            new("Consumer", 18, [new("Cloud", 14), new("Desktop", 16), new("Services", 8)])
        };
        _standaloneLegendItems = new ObservableCollection<ChartLegendItem>
        {
            new()
            {
                Text = "Signal",
                MarkerShape = LegendMarkerShape.Line,
                Stroke = Brushes.DodgerBlue
            },
            new()
            {
                Text = "Events",
                MarkerShape = LegendMarkerShape.Circle,
                Fill = Brushes.MediumPurple,
                Stroke = Brushes.MediumPurple
            },
            new()
            {
                Text = "Band",
                MarkerShape = LegendMarkerShape.Band,
                Fill = new SolidColorBrush(Color.FromArgb(80, 33, 150, 243)),
                Stroke = Brushes.DodgerBlue,
                SecondaryFill = new SolidColorBrush(Color.FromArgb(60, 76, 175, 80)),
                SecondaryStroke = Brushes.ForestGreen
            },
            new()
            {
                Text = "OHLC",
                MarkerShape = LegendMarkerShape.Ohlc,
                Stroke = Brushes.SaddleBrown
            },
            new()
            {
                Text = "Radar",
                MarkerShape = LegendMarkerShape.Radar,
                Fill = new SolidColorBrush(Color.FromArgb(50, 0, 150, 136)),
                Stroke = Brushes.Teal
            }
        };
    }

    [RelayCommand]
    private void ToggleLegend()
    {
        IsLegendVisible = !IsLegendVisible;
    }

    [RelayCommand]
    private void ToggleSeries1()
    {
        IsSeries1Visible = !IsSeries1Visible;
    }
}

public record LegendMekkoItem(string Category, double Width, LegendMekkoSegment[] Segments);

public record LegendMekkoSegment(string Name, double Value);
