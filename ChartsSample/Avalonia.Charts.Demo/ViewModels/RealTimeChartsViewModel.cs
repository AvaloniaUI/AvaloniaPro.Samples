using System.Collections.ObjectModel;
using Avalonia.Media;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class RealTimeChartsViewModel : ViewModelBase, IDisposable
{
    private readonly Random _random = new();

    // Timers
    private readonly DispatcherTimer _lineChartTimer;
    private readonly DispatcherTimer _gaugeTimer;
    private readonly DispatcherTimer _barChartTimer;
    private readonly DispatcherTimer _areaChartTimer;
    private readonly DispatcherTimer _scatterTimer;
    private readonly DispatcherTimer _stackedAreaTimer;
    private readonly DispatcherTimer _cyberAttackTimer;
    private readonly DispatcherTimer _continuousDateTimeTimer;

    private readonly DispatcherTimer _highFreqTimer;
    private readonly DispatcherTimer _barRaceTimer;

    // High Frequency
    private readonly List<double> _highFreqValues = new();
    private double _continuousDateTimeValue = 45;

    // Transition values for gauges
    private double _cpuTarget, _memoryTarget, _networkTarget, _diskTarget;
    private double _cpuCurrent, _memoryCurrent, _networkCurrent, _diskCurrent;

    private const int MaxDataPoints = 50;

    // Pause/Resume state
    private bool _isPaused;

    private const int HighFreqMaxDataPoints = 1000;
    private const double HighFreqMinY = -10;
    private const double HighFreqMaxY = 90;
    public RealTimeChartsViewModel()
    {
        InitializeData();
        
        _lineChartTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
        _lineChartTimer.Tick += (_, _) => UpdateLineChart();

        _gaugeTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(150) };
        _gaugeTimer.Tick += (_, _) => UpdateGauges();

        _barChartTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1500) };
        _barChartTimer.Tick += (_, _) => UpdateBarChart();

        _areaChartTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
        _areaChartTimer.Tick += (_, _) => UpdateAreaChart();

        _scatterTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
        _scatterTimer.Tick += (_, _) => UpdateScatterChart();

        _stackedAreaTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
        _stackedAreaTimer.Tick += (_, _) => UpdateStackedAreaChart();

        _cyberAttackTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(600) };
        _cyberAttackTimer.Tick += (_, _) => UpdateCyberAttackMap();

        _continuousDateTimeTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(600) };
        _continuousDateTimeTimer.Tick += (_, _) => UpdateContinuousDateTimeChart();
        
        _highFreqTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
        _highFreqTimer.Tick += (_, _) => UpdateHighFreqChart();

        _barRaceTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(250) };
        _barRaceTimer.Tick += (_, _) => UpdateBarRace();

        // Don't auto-start, wait for Resume() call
        _isPaused = true;
    }

    /// <summary>
    /// Pauses all real-time updates (call when page is not visible).
    /// </summary>
    public void Pause()
    {
        if (_isPaused) return;
        _isPaused = true;

        _lineChartTimer.Stop();
        _gaugeTimer.Stop();
        _barChartTimer.Stop();
        _areaChartTimer.Stop();
        _scatterTimer.Stop();
        _stackedAreaTimer.Stop();
        _cyberAttackTimer.Stop();
        _continuousDateTimeTimer.Stop();
        _highFreqTimer.Stop();
        _barRaceTimer.Stop();
    }

    /// <summary>
    /// Resumes all real-time updates (call when page becomes visible).
    /// </summary>
    public void Resume()
    {
        if (!_isPaused) return;
        _isPaused = false;

        _lineChartTimer.Start();
        _gaugeTimer.Start();
        _barChartTimer.Start();
        _areaChartTimer.Start();
        _scatterTimer.Start();
        _stackedAreaTimer.Start();
        _cyberAttackTimer.Start();
        _continuousDateTimeTimer.Start();
        _highFreqTimer.Start();
        _barRaceTimer.Start();
    }

    public void Dispose()
    {
        Pause();
    }

    private void InitializeData()
    {
        InitializeHighFreqData();
        InitializeLineData();
        InitializeGauges();
        InitializeBarData();
        InitializeAreaData();
        InitializeScatterData();
        InitializeStackedData();
        InitializeContinuousDateTimeData();
        InitializeCyberAttackMap();
        InitializeBarRaceData();
    }

    // High Frequency Chart
    
    [ObservableProperty] private ObservableCollection<Point> _highFreqData = new();

    private void InitializeHighFreqData()
    {
        var r = new Random(12345);
        double y = 50;
        _highFreqValues.Clear();

        for (int i = 0; i < HighFreqMaxDataPoints; i++)
        {
            y += (r.NextDouble() - 0.5) * 5;
            y = Math.Clamp(y, HighFreqMinY, HighFreqMaxY);
            _highFreqValues.Add(y);
        }

        RefreshHighFreqData();
    }

    private void UpdateHighFreqChart()
    {
        // Add batch of points (simulating high frequency burst)
        int pointsToAdd = 10;
        double y = _highFreqValues.Count > 0 ? _highFreqValues[^1] : 50;
        
        for (int i = 0; i < pointsToAdd; i++)
        {
            y += (_random.NextDouble() - 0.5) * 5;
            y = Math.Clamp(y, HighFreqMinY, HighFreqMaxY);
            _highFreqValues.Add(y);
        }

        var overflow = _highFreqValues.Count - HighFreqMaxDataPoints;
        if (overflow > 0)
        {
            _highFreqValues.RemoveRange(0, overflow);
        }

        RefreshHighFreqData();
    }

    private void RefreshHighFreqData()
    {
        var points = new ObservableCollection<Point>();

        for (var i = 0; i < _highFreqValues.Count; i++)
        {
            points.Add(new Point(i, _highFreqValues[i]));
        }

        HighFreqData = points;
    }

    // Continuous DateTime Axis

    [ObservableProperty] private ObservableCollection<RealTimeDatePoint> _continuousDateTimeData = new();

    private void InitializeContinuousDateTimeData()
    {
        var timestamp = DateTime.Now.AddSeconds(-30);

        for (var i = 0; i < MaxDataPoints; i++)
        {
            _continuousDateTimeValue += (_random.NextDouble() - 0.5) * 8;
            ContinuousDateTimeData.Add(new RealTimeDatePoint(
                timestamp.AddMilliseconds(i * 600),
                Math.Clamp(_continuousDateTimeValue, 15, 85)));
        }
    }

    private void UpdateContinuousDateTimeChart()
    {
        _continuousDateTimeValue += (_random.NextDouble() - 0.5) * 10;
        _continuousDateTimeValue = Math.Clamp(_continuousDateTimeValue, 15, 85);

        ContinuousDateTimeData.Add(new RealTimeDatePoint(DateTime.Now, _continuousDateTimeValue));

        while (ContinuousDateTimeData.Count > MaxDataPoints)
        {
            ContinuousDateTimeData.RemoveAt(0);
        }
    }

    // Live Line Chart

    [ObservableProperty] private ObservableCollection<double> _lineData1 = new();
    [ObservableProperty] private ObservableCollection<double> _lineData2 = new();

    private void InitializeLineData()
    {
        for (int i = 0; i < MaxDataPoints; i++)
        {
            var val1 = 50 + _random.NextDouble() * 30;
            LineData1.Add(val1);
            LineData2.Add(Math.Max(0, val1 - 15 + _random.NextDouble() * 10));
        }
    }

    private void UpdateLineChart()
    {
        var lastValue = LineData1.Count > 0 ? LineData1[^1] : 50;
        var newValue = lastValue + (_random.NextDouble() - 0.5) * 20;
        newValue = Math.Clamp(newValue, 10, 90);

        LineData1.Add(newValue);
        if (LineData1.Count > MaxDataPoints)
            LineData1.RemoveAt(0);

        var lastValue2 = LineData2.Count > 0 ? LineData2[^1] : 40;
        var newValue2 = lastValue2 + (_random.NextDouble() - 0.5) * 18;
        newValue2 = Math.Clamp(newValue2, 5, 85);

        LineData2.Add(newValue2);
        if (LineData2.Count > MaxDataPoints)
            LineData2.RemoveAt(0);
    }

    // Gauges
    [ObservableProperty] private double _cpuValue;
    [ObservableProperty] private double _memoryValue;
    [ObservableProperty] private double _networkValue;
    [ObservableProperty] private double _diskValue;

    private void InitializeGauges()
    {
        _cpuTarget = _cpuCurrent = 35;
        _memoryTarget = _memoryCurrent = 55;
        _networkTarget = _networkCurrent = 20;
        _diskTarget = _diskCurrent = 45;

        UpdateGaugeValues();
    }

    private void UpdateGauges()
    {
        if (_random.NextDouble() > 0.7)
        {
            _cpuTarget = _random.NextDouble() * 100;
            _memoryTarget = 40 + _random.NextDouble() * 40; 
            _networkTarget = _random.NextDouble() * 80;
            _diskTarget = _random.NextDouble() * 60;
        }

        const double smoothFactor = 0.15;
        _cpuCurrent += (_cpuTarget - _cpuCurrent) * smoothFactor;
        _memoryCurrent += (_memoryTarget - _memoryCurrent) * smoothFactor;
        _networkCurrent += (_networkTarget - _networkCurrent) * smoothFactor;
        _diskCurrent += (_diskTarget - _diskCurrent) * smoothFactor;

        UpdateGaugeValues();
    }

    private void UpdateGaugeValues()
    {
        CpuValue = _cpuCurrent;
        MemoryValue = _memoryCurrent;
        NetworkValue = _networkCurrent;
        DiskValue = _diskCurrent;
    }

    // Live Bar Chart
    [ObservableProperty] private ObservableCollection<double> _barValues = new();

    private void InitializeBarData()
    {
        BarValues = new ObservableCollection<double> { 50, 60, 70, 80, 55 };
    }

    private void UpdateBarChart()
    {
        // Randomly adjust bar values
        for (int i = 0; i < BarValues.Count; i++)
        {
            var val = BarValues[i];
            val += (_random.NextDouble() - 0.5) * 20;
            BarValues[i] = Math.Clamp(val, 10, 100);
        }
    }

    // Live Area Chart
    [ObservableProperty] private ObservableCollection<double> _areaInData = new();
    [ObservableProperty] private ObservableCollection<double> _areaOutData = new();

    private void InitializeAreaData()
    {
        for (int i = 0; i < MaxDataPoints; i++)
        {
            AreaInData.Add(20 + _random.NextDouble() * 30);
            AreaOutData.Add(10 + _random.NextDouble() * 20);
        }
    }

    private void UpdateAreaChart()
    {
        var lastIn = AreaInData.Count > 0 ? AreaInData[^1] : 35;
        var lastOut = AreaOutData.Count > 0 ? AreaOutData[^1] : 15;

        var newIn = lastIn + (_random.NextDouble() - 0.5) * 15;
        var newOut = lastOut + (_random.NextDouble() - 0.5) * 10;

        AreaInData.Add(Math.Clamp(newIn, 5, 80));
        AreaOutData.Add(Math.Clamp(newOut, 2, 50));

        if (AreaInData.Count > MaxDataPoints) AreaInData.RemoveAt(0);
        if (AreaOutData.Count > MaxDataPoints) AreaOutData.RemoveAt(0);
    }

    // Live Scatter Chart
    [ObservableProperty] private ObservableCollection<double> _scatterDataValues = new();

    private void InitializeScatterData()
    {
        for (int i = 0; i < 25; i++)
        {
            ScatterDataValues.Add(_random.NextDouble() * 100);
        }
    }

    private void UpdateScatterChart()
    {
        int changesToMake = _random.Next(3, 8);
        for (int i = 0; i < changesToMake; i++)
        {
            if (ScatterDataValues.Count > 0)
            {
                int index = _random.Next(ScatterDataValues.Count);
                ScatterDataValues[index] = _random.NextDouble() * 100;
            }
        }

        if (_random.NextDouble() > 0.8 && ScatterDataValues.Count < 35)
        {
            ScatterDataValues.Add(_random.NextDouble() * 100);
        }
        else if (_random.NextDouble() > 0.9 && ScatterDataValues.Count > 15)
        {
            ScatterDataValues.RemoveAt(_random.Next(ScatterDataValues.Count));
        }
    }

    // Live Stacked Area Chart
    [ObservableProperty] private ObservableCollection<double> _stackedData1 = new();
    [ObservableProperty] private ObservableCollection<double> _stackedData2 = new();
    [ObservableProperty] private ObservableCollection<double> _stackedData3 = new();

    private void InitializeStackedData()
    {
        for (int i = 0; i < MaxDataPoints; i++)
        {
            StackedData1.Add(20 + _random.NextDouble() * 20);
            StackedData2.Add(15 + _random.NextDouble() * 15);
            StackedData3.Add(10 + _random.NextDouble() * 10);
        }
    }

    private void UpdateStackedAreaChart()
    {
        AddSmoothedValue(StackedData1, 20, 15, 5, 50);
        AddSmoothedValue(StackedData2, 15, 10, 3, 40);
        AddSmoothedValue(StackedData3, 10, 8, 2, 30);

        if (StackedData1.Count > MaxDataPoints) StackedData1.RemoveAt(0);
        if (StackedData2.Count > MaxDataPoints) StackedData2.RemoveAt(0);
        if (StackedData3.Count > MaxDataPoints) StackedData3.RemoveAt(0);
    }

    private void AddSmoothedValue(ObservableCollection<double> data, double baseValue, double variance, double min, double max)
    {
        var last = data.Count > 0 ? data[^1] : baseValue;
        var newVal = last + (_random.NextDouble() - 0.5) * variance;
        data.Add(Math.Clamp(newVal, min, max));
    }

    // Cyber Attack Map
    [ObservableProperty] private ObservableCollection<MapArc> _attackArcs = new();
    [ObservableProperty] private ObservableCollection<HeatmapPoint> _heatmapPoints = new();
    [ObservableProperty] private ObservableCollection<MapMarker> _attackMarkers = new();

    private void InitializeCyberAttackMap()
    {
        // Attack Markers
        AttackMarkers = new ObservableCollection<MapMarker>
        {
            // Attack sources - RED
            new MapMarker { Latitude = 55.75, Longitude = 37.62, MarkerType = MapIconType.Circle, MarkerSize = 16, Fill = new SolidColorBrush(Color.FromArgb(60, 255, 0, 0)), Label = "" },
            new MapMarker { Latitude = 55.75, Longitude = 37.62, MarkerType = MapIconType.Circle, MarkerSize = 10, Fill = Brushes.Red, Label = "RU" },       
            
            new MapMarker { Latitude = 39.90, Longitude = 116.41, MarkerType = MapIconType.Circle, MarkerSize = 16, Fill = new SolidColorBrush(Color.FromArgb(60, 255, 0, 0)), Label = "" },
            new MapMarker { Latitude = 39.90, Longitude = 116.41, MarkerType = MapIconType.Circle, MarkerSize = 10, Fill = Brushes.Red, Label = "CN" },       
            
            new MapMarker { Latitude = 35.69, Longitude = 51.39, MarkerType = MapIconType.Circle, MarkerSize = 12, Fill = new SolidColorBrush(Color.FromArgb(60, 255, 69, 0)), Label = "" },
            new MapMarker { Latitude = 35.69, Longitude = 51.39, MarkerType = MapIconType.Circle, MarkerSize = 7, Fill = Brushes.OrangeRed, Label = "IR" },
            
            new MapMarker { Latitude = 39.02, Longitude = 125.75, MarkerType = MapIconType.Circle, MarkerSize = 12, Fill = new SolidColorBrush(Color.FromArgb(60, 255, 0, 0)), Label = "" },
            new MapMarker { Latitude = 39.02, Longitude = 125.75, MarkerType = MapIconType.Circle, MarkerSize = 7, Fill = Brushes.Red, Label = "KP" },
            
            // Attack targets - CYAN/GREEN
            new MapMarker { Latitude = 40.71, Longitude = -74.01, MarkerType = MapIconType.Circle, MarkerSize = 18, Fill = new SolidColorBrush(Color.FromArgb(40, 0, 255, 255)), Label = "" },
            new MapMarker { Latitude = 40.71, Longitude = -74.01, MarkerType = MapIconType.Circle, MarkerSize = 10, Fill = Brushes.Cyan, Label = "NYC" },
            
            new MapMarker { Latitude = 37.77, Longitude = -122.42, MarkerType = MapIconType.Circle, MarkerSize = 18, Fill = new SolidColorBrush(Color.FromArgb(40, 0, 255, 0)), Label = "" },
            new MapMarker { Latitude = 37.77, Longitude = -122.42, MarkerType = MapIconType.Circle, MarkerSize = 10, Fill = Brushes.Lime, Label = "SFO" },
            
            new MapMarker { Latitude = 51.51, Longitude = -0.13, MarkerType = MapIconType.Circle, MarkerSize = 16, Fill = new SolidColorBrush(Color.FromArgb(40, 0, 255, 255)), Label = "" },
            new MapMarker { Latitude = 51.51, Longitude = -0.13, MarkerType = MapIconType.Circle, MarkerSize = 9, Fill = Brushes.Cyan, Label = "LDN" },
            
            new MapMarker { Latitude = 35.68, Longitude = 139.69, MarkerType = MapIconType.Circle, MarkerSize = 16, Fill = new SolidColorBrush(Color.FromArgb(40, 0, 255, 255)), Label = "" },
            new MapMarker { Latitude = 35.68, Longitude = 139.69, MarkerType = MapIconType.Circle, MarkerSize = 9, Fill = Brushes.Cyan, Label = "TYO" },
            
            new MapMarker { Latitude = 52.52, Longitude = 13.41, MarkerType = MapIconType.Circle, MarkerSize = 14, Fill = new SolidColorBrush(Color.FromArgb(40, 0, 255, 0)), Label = "" },
            new MapMarker { Latitude = 52.52, Longitude = 13.41, MarkerType = MapIconType.Circle, MarkerSize = 8, Fill = Brushes.Lime, Label = "BER" },
            
            new MapMarker { Latitude = 48.86, Longitude = 2.35, MarkerType = MapIconType.Circle, MarkerSize = 14, Fill = new SolidColorBrush(Color.FromArgb(40, 0, 255, 255)), Label = "" },
            new MapMarker { Latitude = 48.86, Longitude = 2.35, MarkerType = MapIconType.Circle, MarkerSize = 8, Fill = Brushes.Cyan, Label = "PAR" },
            
            new MapMarker { Latitude = -33.87, Longitude = 151.21, MarkerType = MapIconType.Circle, MarkerSize = 14, Fill = new SolidColorBrush(Color.FromArgb(40, 0, 255, 0)), Label = "" },
            new MapMarker { Latitude = -33.87, Longitude = 151.21, MarkerType = MapIconType.Circle, MarkerSize = 8, Fill = Brushes.Lime, Label = "SYD" },
        };

        // Heatmap Data
        HeatmapPoints = new ObservableCollection<HeatmapPoint>
        {
            new(40.71, -74.01, 95.0),   // NYC
            new(37.77, -122.42, 90.0),  // San Francisco
            new(51.51, -0.13, 85.0),    // London
            new(35.68, 139.69, 80.0),   // Tokyo
            new(52.52, 13.41, 70.0),    // Berlin
            new(48.86, 2.35, 65.0),     // Paris
            new(-33.87, 151.21, 55.0),  // Sydney
            new(55.75, 37.62, 40.0),    // Moscow
            new(39.90, 116.41, 45.0),   // Beijing
        };
    }

    private void UpdateCyberAttackMap()
    {
        var attackData = new[]
        {
            (55.75, 37.62, 40.71, -74.01, "DDoS"),
            (55.75, 37.62, 51.51, -0.13, "Malware"),
            (55.75, 37.62, 52.52, 13.41, "Ransomware"),
            (55.75, 37.62, 48.86, 2.35, "Phishing"),
            (55.75, 37.62, 37.77, -122.42, "Zero-Day"),
            (39.90, 116.41, 37.77, -122.42, "SQL Injection"),
            (39.90, 116.41, 35.68, 139.69, "Malware"),
            (39.90, 116.41, 40.71, -74.01, "DDoS"),
            (39.90, 116.41, 51.51, -0.13, "Botnet"),
            (31.23, 121.47, -33.87, 151.21, "Phishing"),
            (31.23, 121.47, 1.35, 103.82, "Malware"),
            (39.02, 125.75, 40.71, -74.01, "Ransomware"),
            (39.02, 125.75, 35.68, 139.69, "DDoS"),
            (39.02, 125.75, 51.51, -0.13, "Zero-Day"),
            (35.69, 51.39, 52.52, 13.41, "Malware"),
            (35.69, 51.39, 48.86, 2.35, "DDoS"),
            (35.69, 51.39, 51.51, -0.13, "Ransomware"),
            (51.51, -0.13, 35.68, 139.69, "Botnet"),
            (40.71, -74.01, 35.68, 139.69, "SQL Injection"),
        };

        while (AttackArcs.Count > 25)
        {
            AttackArcs.RemoveAt(0);
        }

        var count = _random.Next(3, 7);
        for (var i = 0; i < count; i++)
        {
            var attack = attackData[_random.Next(attackData.Length)];
            
            IBrush strokeColor = attack.Item5 switch
            {
                "DDoS" => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 255, 0, 80)),
                "Malware" => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 255, 50, 0)),
                "Ransomware" => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 255, 0, 0)),
                "Phishing" => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 255, 200, 0)),
                "SQL Injection" => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 150, 0, 255)),
                "Zero-Day" => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 0, 255, 255)),
                "Botnet" => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 0, 255, 100)),
                _ => new SolidColorBrush(Color.FromArgb((byte)(180 + _random.Next(75)), 255, 100, 100))
            };
            
            var arc = new MapArc
            {
                FromLatitude = attack.Item1,
                FromLongitude = attack.Item2,
                ToLatitude = attack.Item3,
                ToLongitude = attack.Item4,
                Curvature = 0.08 + _random.NextDouble() * 0.15,
                StrokeThickness = 1.0 + _random.NextDouble() * 2.5,
                Stroke = strokeColor
            };
            AttackArcs.Add(arc);
        }
    }


    // Bar Race Chart
    [ObservableProperty] private ObservableCollection<BarRaceItem> _barRaceData = new();
    [ObservableProperty] private int _currentYear;
    
    private const int StartYear = 1980;
    private const int EndYear = 2025;
    private const double BarRaceYearStep = 0.5;
    private double _barRaceYearPosition;
    
    // Initial GDP-like dataset (simulated)
    private readonly List<BarRaceItem> _allCountries = new()
    {
        new BarRaceItem("USA", 6000, Brushes.CornflowerBlue),
        new BarRaceItem("China", 3000, Brushes.Crimson),
        new BarRaceItem("Japan", 4000, Brushes.IndianRed),
        new BarRaceItem("Germany", 3500, Brushes.Gold),
        new BarRaceItem("UK", 3000, Brushes.RoyalBlue),
        new BarRaceItem("India", 1500, Brushes.Orange),
        new BarRaceItem("France", 2800, Brushes.MediumPurple),
        new BarRaceItem("Brazil", 2000, Brushes.ForestGreen),
        new BarRaceItem("Italy", 2500, Brushes.SeaGreen),
        new BarRaceItem("Canada", 2200, Brushes.Salmon),
    };

    private void InitializeBarRaceData()
    {
        ResetBarRace();
    }

    private void ResetBarRace()
    {
        _barRaceYearPosition = StartYear;
        CurrentYear = StartYear;
        _allCountries[0].Value = 6000; // USA
        _allCountries[1].Value = 1000; // China (starts lower)
        _allCountries[2].Value = 4000; // Japan
        _allCountries[3].Value = 3500; // Germany
        _allCountries[4].Value = 3000; // UK
        _allCountries[5].Value = 500;  // India (starts lower)
        _allCountries[6].Value = 2800; // France
        _allCountries[7].Value = 2000; // Brazil
        _allCountries[8].Value = 2500; // Italy
        _allCountries[9].Value = 2200; // Canada

        BarRaceData.Clear();
        foreach (var item in _allCountries.OrderByDescending(x => x.Value))
        {
            BarRaceData.Add(item);
        }
    }

    private void UpdateBarRace()
    {
        if (_barRaceYearPosition >= EndYear)
        {
            ResetBarRace();
            return;
        }

        _barRaceYearPosition = Math.Min(EndYear, _barRaceYearPosition + BarRaceYearStep);
        CurrentYear = (int)Math.Floor(_barRaceYearPosition);

        _allCountries[0].Value *= Math.Pow(1.02, BarRaceYearStep);
        _allCountries[1].Value *= Math.Pow(1.08, BarRaceYearStep);
        _allCountries[2].Value *= Math.Pow(1.01, BarRaceYearStep);
        _allCountries[3].Value *= Math.Pow(1.015, BarRaceYearStep);
        _allCountries[4].Value *= Math.Pow(1.015, BarRaceYearStep);
        _allCountries[5].Value *= Math.Pow(1.07, BarRaceYearStep);
        _allCountries[6].Value *= Math.Pow(1.015, BarRaceYearStep);
        _allCountries[7].Value *= Math.Pow(1.025, BarRaceYearStep);
        _allCountries[8].Value *= Math.Pow(1.01, BarRaceYearStep);
        _allCountries[9].Value *= Math.Pow(1.02, BarRaceYearStep);

        UpdateBarRaceCollection();
    }

    private void UpdateBarRaceCollection()
    {
        var sorted = _allCountries.OrderByDescending(x => x.Value).ToList();

        for (var i = 0; i < sorted.Count; i++)
        {
            var item = sorted[i];
            var oldIndex = BarRaceData.IndexOf(item);
            if (oldIndex == i)
            {
                continue;
            }

            if (oldIndex >= 0)
            {
                BarRaceData.Move(oldIndex, i);
            }
            else
            {
                BarRaceData.Insert(i, item);
            }
        }

        while (BarRaceData.Count > sorted.Count)
        {
            BarRaceData.RemoveAt(BarRaceData.Count - 1);
        }
    }
}

public class HeatmapPoint
{
    public double Lat { get; set; }
    public double Lon { get; set; }
    public double Heat { get; set; }

    public HeatmapPoint(double lat, double lon, double heat)
    {
        Lat = lat;
        Lon = lon;
        Heat = heat;
    }
}

public class BarRaceItem : ObservableObject
{
    private double _value;

    public string Name { get; set; }
    
    public double Value 
    { 
        get => _value;
        set => SetProperty(ref _value, value);
    }
    
    public IBrush Color { get; set; }

    public BarRaceItem(string name, double value, IBrush color)
    {
        Name = name;
        Value = value;
        Color = color;
    }
}

public record RealTimeDatePoint(DateTime Timestamp, double Value);
