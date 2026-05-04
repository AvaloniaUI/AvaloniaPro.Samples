using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class MarkersViewModel : ViewModelBase
{
    public MarkersViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // All Markers Chart
        CircleMarkerData = new ObservableCollection<int> { 20, 35, 25, 40, 30 };
        SquareMarkerData = new ObservableCollection<int> { 25, 40, 30, 45, 35 };
        DiamondMarkerData = new ObservableCollection<int> { 30, 45, 35, 50, 40 };
        TriangleMarkerData = new ObservableCollection<int> { 15, 30, 20, 35, 25 };
        PentagonMarkerData = new ObservableCollection<int> { 35, 50, 40, 55, 45 };

        // Single Marker Charts
        CircleChartData = new ObservableCollection<int> { 45, 52, 38, 65, 70, 55, 80 };
        SquareChartData = new ObservableCollection<int> { 30, 45, 35, 55, 48, 62, 58 };
        DiamondChartData = new ObservableCollection<int> { 25, 40, 32, 48, 55, 45, 60 };

        // Triangle Chart
        UpTrendData = new ObservableCollection<int> { 20, 35, 45, 55, 70, 80, 95 };
        DownTrendData = new ObservableCollection<int> { 90, 75, 65, 55, 40, 30, 15 };

        // Cross Marker Chart
        CrossChartData = new ObservableCollection<int> { 35, 50, 42, 58, 48, 65, 55 };

        // Pentagon Marker Chart
        PentagonChartData = new ObservableCollection<int> { 40, 55, 48, 62, 55, 70, 65 };

        // Line Marker Chart
        VerticalLineData = new ObservableCollection<int> { 30, 45, 38, 52, 48, 60, 55 };
        HorizontalLineData = new ObservableCollection<int> { 25, 40, 33, 47, 43, 55, 50 };

        // Custom Size Markers
        SmallMarkerData = new ObservableCollection<int> { 20, 30, 25, 35, 30 };
        MediumMarkerData = new ObservableCollection<int> { 30, 40, 35, 45, 40 };
        LargeMarkerData = new ObservableCollection<int> { 40, 50, 45, 55, 50 };
    }

    // All Markers Chart
    [ObservableProperty] private ObservableCollection<int> _circleMarkerData = null!;
    [ObservableProperty] private ObservableCollection<int> _squareMarkerData = null!;
    [ObservableProperty] private ObservableCollection<int> _diamondMarkerData = null!;
    [ObservableProperty] private ObservableCollection<int> _triangleMarkerData = null!;
    [ObservableProperty] private ObservableCollection<int> _pentagonMarkerData = null!;

    // Single Marker Charts
    [ObservableProperty] private ObservableCollection<int> _circleChartData = null!;
    [ObservableProperty] private ObservableCollection<int> _squareChartData = null!;
    [ObservableProperty] private ObservableCollection<int> _diamondChartData = null!;

    // Triangle Chart
    [ObservableProperty] private ObservableCollection<int> _upTrendData = null!;
    [ObservableProperty] private ObservableCollection<int> _downTrendData = null!;

    // Cross Marker Chart
    [ObservableProperty] private ObservableCollection<int> _crossChartData = null!;

    // Pentagon Marker Chart
    [ObservableProperty] private ObservableCollection<int> _pentagonChartData = null!;

    // Line Marker Chart
    [ObservableProperty] private ObservableCollection<int> _verticalLineData = null!;
    [ObservableProperty] private ObservableCollection<int> _horizontalLineData = null!;

    // Custom Size Markers
    [ObservableProperty] private ObservableCollection<int> _smallMarkerData = null!;
    [ObservableProperty] private ObservableCollection<int> _mediumMarkerData = null!;
    [ObservableProperty] private ObservableCollection<int> _largeMarkerData = null!;
}
