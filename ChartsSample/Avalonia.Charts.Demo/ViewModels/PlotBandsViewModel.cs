using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class PlotBandsViewModel : ViewModelBase
{
    public PlotBandsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Horizontal Bands Chart - Temperature data
        HorizontalBandsData = new ObservableCollection<int> { 12, 18, 22, 28, 32, 26, 20, 15, 10, 8, 14, 19 };

        // Vertical Bands Chart - Sales data
        VerticalBandsData = new ObservableCollection<int> { 150, 180, 165, 190, 175, 80, 60 };

        // Text Bands Chart - Stock Price data
        TextBandsData = new ObservableCollection<int> { 55, 62, 58, 75, 82, 95, 88, 72, 65, 58 };

        // Repeating Bands Chart - Monthly data
        RepeatingBandsData = new ObservableCollection<int> { 45, 52, 48, 60, 55, 70, 65, 75, 68, 80, 72, 85 };

        // Above Series Chart - CPU Usage data
        AboveSeriesData = new ObservableCollection<int> { 45, 52, 68, 75, 82, 95, 88, 72, 65, 58 };

        // Trend Analysis - Sales data
        TrendData = new ObservableCollection<double> { 85, 88, 92, 90, 95, 98, 102, 105, 103, 110, 115, 112 };
    }

    [ObservableProperty] private ObservableCollection<int> _horizontalBandsData = null!;
    [ObservableProperty] private ObservableCollection<int> _verticalBandsData = null!;
    [ObservableProperty] private ObservableCollection<int> _textBandsData = null!;
    [ObservableProperty] private ObservableCollection<int> _repeatingBandsData = null!;
    [ObservableProperty] private ObservableCollection<int> _aboveSeriesData = null!;
    [ObservableProperty] private ObservableCollection<double> _trendData = null!;
}
