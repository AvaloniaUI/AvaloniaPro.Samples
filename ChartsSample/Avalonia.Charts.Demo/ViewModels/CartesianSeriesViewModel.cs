using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class CartesianSeriesViewModel : ViewModelBase
{
    public CartesianSeriesViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Line Series
        LineSeries2023 = new ObservableCollection<int> { 45, 52, 48, 60, 55, 70, 65, 75, 68, 80, 72, 85 };
        LineSeries2024 = new ObservableCollection<int> { 50, 58, 55, 68, 62, 78, 72, 82, 75, 88, 80, 92 };

        // Bar Series
        BarSeriesData = new ObservableCollection<int> { 150, 180, 165, 190, 175, 200 };

        // Area Series
        AreaSeriesData = new ObservableCollection<int> { 120, 150, 135, 180, 165, 200, 185, 220 };

        // Scatter Series
        ScatterSeriesData = new ObservableCollection<int> { 25, 45, 35, 55, 40, 60, 50, 70, 55, 75 };

        // Scatter Line Series
        ScatterLineSeriesData = new ObservableCollection<NumericValuePoint>
        {
            new(1.0, 18.0),
            new(2.3, 24.0),
            new(3.1, 21.0),
            new(4.8, 33.0),
            new(6.2, 30.0),
            new(7.4, 39.0),
            new(9.0, 35.0)
        };

        // Dot Plot Series
        DotPlotData = new ObservableCollection<CategoryValuePoint>
        {
            new("Mon", 4),
            new("Tue", 6),
            new("Wed", 5),
            new("Thu", 8),
            new("Fri", 7),
            new("Sat", 9)
        };

        // Lollipop Series
        LollipopData = new ObservableCollection<CategoryValuePoint>
        {
            new("North", 42),
            new("South", 57),
            new("East", 49),
            new("West", 61),
            new("Online", 54)
        };

        // Spline Series
        SplineSeriesData = new ObservableCollection<int> { 15, 18, 22, 28, 32, 35, 33, 28, 22, 17, 12, 10 };

        // Spline Area Series
        SplineAreaSeriesData = new ObservableCollection<int> { 1200, 1400, 1100, 1600, 1800, 1500, 2000 };

        // Step Line Series
        StepLineSeriesData = new ObservableCollection<int> { 100, 100, 120, 120, 120, 140, 140, 160 };

        // Stacked Bar Series
        StackedBarProductA = new ObservableCollection<int> { 40, 55, 50, 60, 45, 70 };
        StackedBarProductB = new ObservableCollection<int> { 30, 35, 40, 45, 35, 50 };
        StackedBarProductC = new ObservableCollection<int> { 20, 25, 30, 25, 30, 35 };

        // Stacked 100 Percent Bar Series
        Stacked100North = new ObservableCollection<CategoryValuePoint>
        {
            new("Q1", 28),
            new("Q2", 35),
            new("Q3", 31),
            new("Q4", 26)
        };
        Stacked100South = new ObservableCollection<CategoryValuePoint>
        {
            new("Q1", 42),
            new("Q2", 38),
            new("Q3", 44),
            new("Q4", 47)
        };
        Stacked100West = new ObservableCollection<CategoryValuePoint>
        {
            new("Q1", 30),
            new("Q2", 27),
            new("Q3", 25),
            new("Q4", 27)
        };

        // Stacked Area Series
        StackedAreaDesktop = new ObservableCollection<int> { 500, 480, 520, 510, 490, 530, 550 };
        StackedAreaMobile = new ObservableCollection<int> { 300, 350, 380, 420, 450, 480, 520 };
        StackedAreaTablet = new ObservableCollection<int> { 100, 120, 130, 140, 150, 160, 170 };

        // Range Area Series
        RangeAreaData = new ObservableCollection<CategoryRangePoint>
        {
            new CategoryRangePoint("Jan", 5.0, 15.0),
            new CategoryRangePoint("Feb", 8.0, 18.0),
            new CategoryRangePoint("Mar", 12.0, 25.0),
            new CategoryRangePoint("Apr", 18.0, 30.0),
            new CategoryRangePoint("May", 22.0, 35.0),
            new CategoryRangePoint("Jun", 20.0, 32.0),
            new CategoryRangePoint("Jul", 15.0, 28.0)
        };

        // Range Bar Series
        RangeBarData = new ObservableCollection<CategoryRangePoint>
        {
            new("Sprint 1", 12.0, 18.0),
            new("Sprint 2", 16.0, 24.0),
            new("Sprint 3", 14.0, 27.0),
            new("Sprint 4", 20.0, 32.0),
            new("Sprint 5", 19.0, 29.0)
        };

        // Variance Series
        VarianceData = new ObservableCollection<VariancePoint>
        {
            new VariancePoint("Jan", 12.0),
            new VariancePoint("Feb", -8.0),
            new VariancePoint("Mar", 25.0),
            new VariancePoint("Apr", -3.0),
            new VariancePoint("May", 18.0),
            new VariancePoint("Jun", -15.0),
            new VariancePoint("Jul", 8.0),
            new VariancePoint("Aug", 22.0)
        };

        // Waterfall Series
        WaterfallData = new ObservableCollection<WaterfallPoint>
        {
            new WaterfallPoint("Start", 100.0),
            new WaterfallPoint("Q1", 25.0),
            new WaterfallPoint("Q2", -10.0),
            new WaterfallPoint("Q3", 15.0),
            new WaterfallPoint("Q4", 30.0)
        };

        // Box Plot Series
        BoxPlotData = new ObservableCollection<BoxPlotPoint>
        {
            new BoxPlotPoint("Q1", 10.0, 25.0, 35.0, 45.0, 60.0),
            new BoxPlotPoint("Q2", 15.0, 30.0, 42.0, 55.0, 70.0),
            new BoxPlotPoint("Q3", 20.0, 35.0, 48.0, 60.0, 75.0),
            new BoxPlotPoint("Q4", 25.0, 40.0, 52.0, 65.0, 80.0)
        };

        // Pictorial Bar Series (Vehicles)
        var pathCycle = StreamGeometry.Parse("M15.5,5.5C16.6,5.5 17.5,4.6 17.5,3.5C17.5,2.4 16.6,1.5 15.5,1.5C14.4,1.5 13.5,2.4 13.5,3.5C13.5,4.6 14.4,5.5 15.5,5.5M5,12C2.2,12 0,14.2 0,17C0,19.8 2.2,22 5,22C7.8,22 10,19.8 10,17C10,14.2 7.8,12 5,12M5,20.5C3.1,20.5 1.5,18.9 1.5,17C1.5,15.1 3.1,13.5 5,13.5C6.9,13.5 8.5,15.1 8.5,17C8.5,18.9 6.9,20.5 5,20.5M10.8,12L13.2,9.6L14,10.4C15.3,11.7 17,12.5 19.1,12.5V9C17.6,9 16.4,8.4 15.5,7.5L13.6,5.6C13.1,5.2 12.4,5.2 12,5.6L10.4,7.2L8.3,13.2L12,14.3L13.6,10.9L14.5,12.7L12.5,17.1L12,21L16,23L18,23V21H16.5L14.5,20L18,12.5C18.2,12.7 18.4,12.9 18.6,13V11C20.7,11 22.3,12.9 22,15.1C21.7,16.7 20.3,18 18.6,18C16.7,18 15.1,16.4 15.1,14.5H13.6C13.6,17.3 15.8,19.5 18.6,19.5C21.2,19.5 23.4,17.5 23.6,15C23.8,12 21.4,9.5 18.4,9.5H16.3L10.8,12Z");
        var pathCar = StreamGeometry.Parse("M18.92,6.01C18.72,5.42 18.16,5 17.5,5h-11c-0.66,0-1.21,0.42-1.42,1.01L3,12v8c0,0.55,0.45,1,1,1h1c0.55,0,1-0.45,1-1v-1h12v1 c0,0.55,0.45,1,1,1h1c0.55,0,1-0.45,1-1v-8L18.92,6.01z M6.5,16c-0.83,0-1.5-0.67-1.5-1.5S5.67,13,6.5,13S8,13.67,8,14.5 S7.33,16,6.5,16z M17.5,16c-0.83,0-1.5-0.67-1.5-1.5s0.67-1.5,1.5-1.5s1.5,0.67,1.5,1.5S18.33,16,17.5,16z M5,11l1.5-4.5h11L19,11H5z");
        var pathBus = StreamGeometry.Parse("M4,16c0,0.88,0.39,1.67,1,2.22V20c0,0.55,0.45,1,1,1h1c0.55,0,1-0.45,1-1v-1h8v1c0,0.55,0.45,1,1,1h1c0.55,0,1-0.45,1-1 v-1.78c0.61-0.55,1-1.34,1-2.22V6c0-3.5-3.58-4-8-4S4,2.5,4,6V16z M7.5,17c-0.83,0-1.5-0.67-1.5-1.5S6.67,14,7.5,14S9,14.67,9,15.5 S8.33,17,7.5,17z M16.5,17c-0.83,0-1.5-0.67-1.5-1.5s0.67-1.5,1.5-1.5s1.5,0.67,1.5,1.5S17.33,17,16.5,17z M5,6h14v5H5V6z");
        var pathTrain = StreamGeometry.Parse("M12,2c-4,0-8,0.5-8,4v9.5C4,17.43,5.57,19,7.5,19L6,20.5v0.5h2.23l2-2H14l2,2h2v-0.5L16.5,19c1.93,0,3.5-1.57,3.5-3.5V6 C20,2.5,16,2,12,2z M8,11c-1.1,0-2-0.9-2-2s0.9-2,2-2s2,0.9,2,2S9.1,11,8,11z M16,11c-1.1,0-2-0.9-2-2s0.9-2,2-2s2,0.9,2,2 S17.1,11,16,11z");
        var pathPlane = StreamGeometry.Parse("M21,16v-2l-8-5V3.5C13,2.67,12.33,2,11.5,2S10,2.67,10,3.5V9l-8,5v2l8-2.5V19l-2,1.5V22l3.5-1l3.5,1v-1.5L13,19v-5.5L21,16z");

        PictorialBarData = new ObservableCollection<VehicleItem>
        {
            new VehicleItem("Cycle", 157, 184, pathCycle),
            new VehicleItem("Car", 123, 95, pathCar),
            new VehicleItem("Bus", 78, 91, pathBus),
            new VehicleItem("Train", 45, 66, pathTrain),
            new VehicleItem("Airplane", 66, 73, pathPlane)
        };
    }

    [ObservableProperty] private ObservableCollection<int> _lineSeries2023 = null!;
    [ObservableProperty] private ObservableCollection<int> _lineSeries2024 = null!;
    [ObservableProperty] private ObservableCollection<int> _barSeriesData = null!;
    [ObservableProperty] private ObservableCollection<int> _areaSeriesData = null!;
    [ObservableProperty] private ObservableCollection<int> _scatterSeriesData = null!;
    [ObservableProperty] private ObservableCollection<NumericValuePoint> _scatterLineSeriesData = null!;
    [ObservableProperty] private ObservableCollection<CategoryValuePoint> _dotPlotData = null!;
    [ObservableProperty] private ObservableCollection<CategoryValuePoint> _lollipopData = null!;
    [ObservableProperty] private ObservableCollection<int> _splineSeriesData = null!;
    [ObservableProperty] private ObservableCollection<int> _splineAreaSeriesData = null!;
    [ObservableProperty] private ObservableCollection<int> _stepLineSeriesData = null!;
    [ObservableProperty] private ObservableCollection<int> _stackedBarProductA = null!;
    [ObservableProperty] private ObservableCollection<int> _stackedBarProductB = null!;
    [ObservableProperty] private ObservableCollection<int> _stackedBarProductC = null!;
    [ObservableProperty] private ObservableCollection<CategoryValuePoint> _stacked100North = null!;
    [ObservableProperty] private ObservableCollection<CategoryValuePoint> _stacked100South = null!;
    [ObservableProperty] private ObservableCollection<CategoryValuePoint> _stacked100West = null!;
    [ObservableProperty] private ObservableCollection<int> _stackedAreaDesktop = null!;
    [ObservableProperty] private ObservableCollection<int> _stackedAreaMobile = null!;
    [ObservableProperty] private ObservableCollection<int> _stackedAreaTablet = null!;
    [ObservableProperty] private ObservableCollection<CategoryRangePoint> _rangeAreaData = null!;
    [ObservableProperty] private ObservableCollection<CategoryRangePoint> _rangeBarData = null!;
    [ObservableProperty] private ObservableCollection<VariancePoint> _varianceData = null!;
    [ObservableProperty] private ObservableCollection<WaterfallPoint> _waterfallData = null!;
    [ObservableProperty] private ObservableCollection<BoxPlotPoint> _boxPlotData = null!;
    [ObservableProperty] private ObservableCollection<VehicleItem> _pictorialBarData = null!;
    [ObservableProperty] private Geometry _pictorialBarGeometry = null!;
}

public record CategoryValuePoint(string Category, double Value);
public record NumericValuePoint(double X, double Value);
public record CategoryRangePoint(string Category, double Low, double High);
public record VariancePoint(string Month, double Variance);
public record WaterfallPoint(string Label, double Value);
public record BoxPlotPoint(string Category, double Min, double Q1, double Median, double Q3, double Max);
public record VehicleItem(string Category, double Value2023, double Value2024, Geometry Icon);
