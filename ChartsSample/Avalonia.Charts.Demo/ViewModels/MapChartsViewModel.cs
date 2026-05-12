using System.Collections.ObjectModel;
using Avalonia.Charts.Demo.Models;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class MapChartsViewModel : ViewModelBase
{
    private readonly ShapeLayer _dynamicBaseLayer;
    private readonly MarkerLayer _dynamicMarkerLayer;
    private readonly VectorLayer _dynamicRouteLayer;
    private readonly HeatmapLayer _dynamicHeatLayer;
    private bool _dynamicRoutesVisible = true;
    private bool _dynamicHeatVisible;
    private bool _usingAlternateDynamicMarkers;
    private int _dynamicRouteIndex;

    [ObservableProperty]
    private CountryDensityData[] _shapeLayerData = null!;

    [ObservableProperty]
    private RegionData[] _spainRegionsData = null!;

    [ObservableProperty]
    private ObservableCollection<MapMarker> _spainMarkers = null!;

    [ObservableProperty]
    private ObservableCollection<MapMarker> _airportMarkers = null!;

    [ObservableProperty]
    private CityData[] _cityBubbles = null!;

    [ObservableProperty]
    private RouteData[] _tradeRoutes = null!;

    [ObservableProperty]
    private EarthquakeItem[] _earthquakeData = null!;

    [ObservableProperty]
    private GdpItem[] _gdpData = null!;

    [ObservableProperty]
    private ExchangeItem[] _financialCenters = null!;

    [ObservableProperty]
    private InvestmentFlowItem[] _investmentFlows = null!;

    [ObservableProperty]
    private CountryData[] _interactiveMapData = null!;

    [ObservableProperty]
    private ObservableCollection<MapMarker> _interactiveMarkers = null!;

    [ObservableProperty]
    private List<Color> _pieChartPalette = null!;

    [ObservableProperty]
    private ObservableCollection<LegendItem> _pieChartLegend = null!;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectionMessage))]
    private CountryData? _selectedCountry;

    [ObservableProperty]
    private CityPieData[] _citiesWithPieData = null!;

    [ObservableProperty]
    private string _seatMapGeoJson = null!;

    [ObservableProperty]
    private string _fuselageGeoJson = null!;

    [ObservableProperty]
    private ObservableCollection<SeatInfo> _seatMapData = null!;

    [ObservableProperty]
    private ObservableCollection<SeatInfo> _selectedSeats = new();

    [ObservableProperty]
    private ObservableCollection<MapLayer> _dynamicMapLayers = new();

    [ObservableProperty]
    private ObservableCollection<MapMarker> _dynamicPrimaryMarkers = null!;

    [ObservableProperty]
    private ObservableCollection<MapMarker> _dynamicAlternateMarkers = null!;

    [ObservableProperty]
    private ObservableCollection<MapArc> _dynamicAttackArcs = null!;

    [ObservableProperty]
    private ObservableCollection<DynamicHeatPoint> _dynamicHeatPoints = null!;

    [ObservableProperty]
    private string _dynamicLayerStatus = string.Empty;

    public string SelectionMessage => SelectedCountry != null 
        ? $"Selected: {SelectedCountry.Name} (Value: {SelectedCountry.Value})" 
        : "Selected: None";
        
    public string SeatSelectionMessage => SelectedSeats != null && SelectedSeats.Count > 0
        ? $"Selected Seats: {string.Join(", ", System.Linq.Enumerable.Select(SelectedSeats, s => s.SeatNumber))} (Total: ${System.Linq.Enumerable.Sum(SelectedSeats, s => s.Price)})"
        : "Select seats to see details";

    public MapChartsViewModel()
    {
        _dynamicBaseLayer = CreateDynamicBaseLayer();
        _dynamicMarkerLayer = CreateDynamicMarkerLayer();
        _dynamicRouteLayer = CreateDynamicRouteLayer();
        _dynamicHeatLayer = CreateDynamicHeatLayer();

        InitializeData();
        InitializeDynamicMapLayers();
        GenerateSeatLayout();
    }
    
    public record CountryData(string Code, string Name, double Value, double Population, double GDP, string Region);
    public record CountryDensityData(string Code, string Name, double Density);
    public record RegionData(string Region, string ShortName, int Population);
    public record CityData(string City, double Lat, double Lon, double Population);
    public record RouteData(double FromLat, double FromLon, double ToLat, double ToLon, double Volume);
    public record ExchangeItem(string Exchange, double Lat, double Lon, double MarketCap);
    public record InvestmentFlowItem(double FromLat, double FromLon, double ToLat, double ToLon, double Flow);
    public record GdpItem(string Code, double GDP);
    public record BubbleData(string Exchange, double Lat, double Lon, double MarketCap);
    public record EarthquakeItem(double Lat, double Lon, double Magnitude);
    public record PieSegment(string Category, double Value);
    public record CityPieData(string Name, double Latitude, double Longitude, ObservableCollection<PieSegment> Data);
    public record LegendItem(string Label, IBrush Fill);
    public record SeatInfo(string SeatNumber, string Class, double Price, string Status);
    public record DynamicHeatPoint(double Latitude, double Longitude, double Intensity);

    [RelayCommand]
    private void AddDynamicRoute()
    {
        var routes = new[]
        {
            new MapArc { FromLatitude = 40.71, FromLongitude = -74.01, ToLatitude = 51.51, ToLongitude = -0.13, Curvature = 0.35 },
            new MapArc { FromLatitude = 35.68, FromLongitude = 139.69, ToLatitude = 37.77, ToLongitude = -122.42, Curvature = 0.45 },
            new MapArc { FromLatitude = 52.52, FromLongitude = 13.41, ToLatitude = 1.36, ToLongitude = 103.99, Curvature = 0.30 },
            new MapArc { FromLatitude = -33.87, FromLongitude = 151.21, ToLatitude = 34.05, ToLongitude = -118.24, Curvature = 0.40 }
        };

        var route = routes[_dynamicRouteIndex % routes.Length];
        route.Stroke = new SolidColorBrush(Color.Parse(_dynamicRouteIndex % 2 == 0 ? "#F97316" : "#22D3EE"));
        route.StrokeThickness = 2.5 + (_dynamicRouteIndex % 3);
        DynamicAttackArcs.Add(route);

        _dynamicRouteIndex++;
        EnsureDynamicRouteLayerVisible();
        DynamicLayerStatus = $"Added route {DynamicAttackArcs.Count}. VectorLayer.Arcs mutated in place.";
    }

    [RelayCommand]
    private void ToggleDynamicRoutes()
    {
        if (_dynamicRoutesVisible)
        {
            DynamicMapLayers.Remove(_dynamicRouteLayer);
            _dynamicRoutesVisible = false;
            DynamicLayerStatus = "Removed the VectorLayer from ShapeMap.Layers.";
            return;
        }

        EnsureDynamicRouteLayerVisible();
        DynamicLayerStatus = "Re-added the VectorLayer to ShapeMap.Layers.";
    }

    [RelayCommand]
    private void ToggleDynamicHeat()
    {
        if (_dynamicHeatVisible)
        {
            DynamicMapLayers.Remove(_dynamicHeatLayer);
            _dynamicHeatVisible = false;
            DynamicLayerStatus = "Removed the HeatmapLayer from ShapeMap.Layers.";
            return;
        }

        DynamicHeatPoints[0] = DynamicHeatPoints[0] with { Intensity = DynamicHeatPoints[0].Intensity + 10 };
        DynamicMapLayers.Add(_dynamicHeatLayer);
        _dynamicHeatVisible = true;
        DynamicLayerStatus = "Added HeatmapLayer and mutated its observable ItemsSource.";
    }

    [RelayCommand]
    private void SwapDynamicMarkers()
    {
        _usingAlternateDynamicMarkers = !_usingAlternateDynamicMarkers;
        _dynamicMarkerLayer.Markers = _usingAlternateDynamicMarkers ? DynamicAlternateMarkers : DynamicPrimaryMarkers;
        DynamicLayerStatus = _usingAlternateDynamicMarkers
            ? "MarkerLayer.Markers rebound to the alternate data source."
            : "MarkerLayer.Markers rebound to the primary data source.";
    }

    [RelayCommand]
    private void ResetDynamicLayers()
    {
        InitializeDynamicMapLayers();
        DynamicLayerStatus = "Reset dynamic layers, marker sources, and route collections.";
    }

    private static ShapeLayer CreateDynamicBaseLayer() =>
        new()
        {
            GeoJson = SampleGeoJsonData.World,
            GeoJsonIdPath = "ISO_A2",
            LowBrush = new SolidColorBrush(Color.Parse("#E8F1F5")),
            HighBrush = new SolidColorBrush(Color.Parse("#E8F1F5")),
            Stroke = new SolidColorBrush(Color.Parse("#90A4AE")),
            StrokeThickness = 0.45
        };

    private MarkerLayer CreateDynamicMarkerLayer() =>
        new()
        {
            Fill = Brushes.DeepSkyBlue,
            Stroke = Brushes.White,
            MarkerSize = 12,
            MarkerType = MapIconType.Circle
        };

    private VectorLayer CreateDynamicRouteLayer() =>
        new()
        {
            IsLineAnimationEnabled = false
        };

    private HeatmapLayer CreateDynamicHeatLayer() =>
        new()
        {
            LatitudePath = nameof(DynamicHeatPoint.Latitude),
            LongitudePath = nameof(DynamicHeatPoint.Longitude),
            IntensityPath = nameof(DynamicHeatPoint.Intensity),
            MaxIntensity = 100,
            Radius = 45,
            LowBrush = new SolidColorBrush(Color.Parse("#3322D3EE")),
            MediumBrush = new SolidColorBrush(Color.Parse("#AAEAB308")),
            HighBrush = new SolidColorBrush(Color.Parse("#CCEF4444"))
        };

    private void InitializeDynamicMapLayers()
    {
        DynamicPrimaryMarkers = new ObservableCollection<MapMarker>
        {
            CreateMarker("NYC", 40.71, -74.01, "#0EA5E9", 12),
            CreateMarker("LDN", 51.51, -0.13, "#22C55E", 12),
            CreateMarker("TYO", 35.68, 139.69, "#A855F7", 12)
        };

        DynamicAlternateMarkers = new ObservableCollection<MapMarker>
        {
            CreateMarker("SFO", 37.77, -122.42, "#F97316", 12),
            CreateMarker("BER", 52.52, 13.41, "#22C55E", 12),
            CreateMarker("SIN", 1.36, 103.99, "#EAB308", 12),
            CreateMarker("SYD", -33.87, 151.21, "#38BDF8", 12)
        };

        DynamicAttackArcs = new ObservableCollection<MapArc>
        {
            new()
            {
                FromLatitude = 40.71,
                FromLongitude = -74.01,
                ToLatitude = 35.68,
                ToLongitude = 139.69,
                Stroke = new SolidColorBrush(Color.Parse("#F43F5E")),
                StrokeThickness = 3,
                Curvature = 0.4
            }
        };

        DynamicHeatPoints = new ObservableCollection<DynamicHeatPoint>
        {
            new(40.71, -74.01, 70),
            new(51.51, -0.13, 55),
            new(35.68, 139.69, 82)
        };

        _dynamicMarkerLayer.Markers = DynamicPrimaryMarkers;
        _dynamicRouteLayer.Arcs = DynamicAttackArcs;
        _dynamicHeatLayer.ItemsSource = DynamicHeatPoints;
        _usingAlternateDynamicMarkers = false;
        _dynamicRoutesVisible = true;
        _dynamicHeatVisible = false;
        _dynamicRouteIndex = 0;

        DynamicMapLayers.Clear();
        DynamicMapLayers.Add(_dynamicBaseLayer);
        DynamicMapLayers.Add(_dynamicMarkerLayer);
        DynamicMapLayers.Add(_dynamicRouteLayer);
        DynamicLayerStatus = "Base ShapeLayer, MarkerLayer, and VectorLayer are active.";
    }

    private static MapMarker CreateMarker(string label, double latitude, double longitude, string color, double size) =>
        new()
        {
            Label = label,
            Latitude = latitude,
            Longitude = longitude,
            MarkerType = MapIconType.Circle,
            MarkerSize = size,
            Fill = new SolidColorBrush(Color.Parse(color)),
            Stroke = Brushes.White
        };

    private void EnsureDynamicRouteLayerVisible()
    {
        if (!DynamicMapLayers.Contains(_dynamicRouteLayer))
        {
            DynamicMapLayers.Add(_dynamicRouteLayer);
        }

        _dynamicRoutesVisible = true;
    }

    private void InitializeData()
    {
        ShapeLayerData = new[]
        {
            new CountryDensityData("IN", "India", 464.0),
            new CountryDensityData("BD", "Bangladesh", 1265.0),
            new CountryDensityData("JP", "Japan", 347.0),
            new CountryDensityData("KR", "S. Korea", 527.0),
            new CountryDensityData("NL", "Netherlands", 508.0),
            new CountryDensityData("BE", "Belgium", 376.0),
            new CountryDensityData("GB", "UK", 275.0),
            new CountryDensityData("DE", "Germany", 240.0),
            new CountryDensityData("IT", "Italy", 206.0),
            new CountryDensityData("FR", "France", 119.0),
            new CountryDensityData("CN", "China", 153.0),
            new CountryDensityData("US", "USA", 36.0),
            new CountryDensityData("CA", "Canada", 4.0),
            new CountryDensityData("BR", "Brazil", 25.0),
            new CountryDensityData("RU", "Russia", 9.0),
            new CountryDensityData("AU", "Australia", 3.0),
        };

        SpainRegionsData = new[]
        {
            new RegionData("Andalucía", "AND", 8500000),
            new RegionData("Cataluña", "CAT", 7800000),
            new RegionData("Comunidad de Madrid", "MAD", 6800000),
            new RegionData("Comunidad Valenciana", "VAL", 5000000),
            new RegionData("Galicia", "GAL", 2700000),
            new RegionData("Castilla y León", "CYL", 2400000),
            new RegionData("País Vasco", "PVA", 2200000),
            new RegionData("Islas Canarias", "CAN", 2200000),
            new RegionData("Castilla-La Mancha", "CLM", 2000000),
            new RegionData("Región de Murcia", "MUR", 1500000),
            new RegionData("Aragón", "ARA", 1300000),
            new RegionData("Islas Baleares", "BAL", 1200000),
            new RegionData("Extremadura", "EXT", 1000000),
            new RegionData("Asturias", "AST", 1000000),
            new RegionData("Navarra", "NAV", 660000),
            new RegionData("Cantabria", "CAN", 580000),
            new RegionData("La Rioja", "RIO", 315000),
        };

        SpainMarkers = new ObservableCollection<MapMarker>
        {
            new MapMarker { Latitude = 40.42, Longitude = -3.70, MarkerType = MapIconType.Circle, MarkerSize = 10, Fill = Brushes.Red, Label = "Madrid" },
            new MapMarker { Latitude = 41.39, Longitude = 2.17, MarkerType = MapIconType.Circle, MarkerSize = 9, Fill = Brushes.Blue, Label = "Barcelona" },
            new MapMarker { Latitude = 39.47, Longitude = -0.38, MarkerType = MapIconType.Circle, MarkerSize = 8, Fill = Brushes.Orange, Label = "Valencia" },
            new MapMarker { Latitude = 37.39, Longitude = -5.99, MarkerType = MapIconType.Circle, MarkerSize = 8, Fill = Brushes.Green, Label = "Sevilla" },
            new MapMarker { Latitude = 43.26, Longitude = -2.93, MarkerType = MapIconType.Circle, MarkerSize = 7, Fill = Brushes.Purple, Label = "Bilbao" },
        };

        AirportMarkers = new ObservableCollection<MapMarker>
        {
            new MapMarker { Latitude = 33.94, Longitude = -118.41, MarkerType = MapIconType.Triangle, MarkerSize = 12, Fill = Brushes.DodgerBlue, Label = "LAX" },
            new MapMarker { Latitude = 40.64, Longitude = -73.78, MarkerType = MapIconType.Triangle, MarkerSize = 12, Fill = Brushes.DodgerBlue, Label = "JFK" },
            new MapMarker { Latitude = 51.47, Longitude = -0.46, MarkerType = MapIconType.Triangle, MarkerSize = 12, Fill = Brushes.Green, Label = "LHR" },
            new MapMarker { Latitude = 49.01, Longitude = 2.55, MarkerType = MapIconType.Triangle, MarkerSize = 12, Fill = Brushes.Green, Label = "CDG" },
            new MapMarker { Latitude = 35.55, Longitude = 139.78, MarkerType = MapIconType.Triangle, MarkerSize = 12, Fill = Brushes.Red, Label = "HND" },
            new MapMarker { Latitude = 25.25, Longitude = 55.36, MarkerType = MapIconType.Diamond, MarkerSize = 14, Fill = Brushes.Gold, Label = "DXB" },
            new MapMarker { Latitude = 1.36, Longitude = 103.99, MarkerType = MapIconType.Diamond, MarkerSize = 14, Fill = Brushes.Gold, Label = "SIN" },
            new MapMarker { Latitude = 22.31, Longitude = 113.92, MarkerType = MapIconType.Triangle, MarkerSize = 12, Fill = Brushes.Red, Label = "HKG" },
            new MapMarker { Latitude = -33.95, Longitude = 151.18, MarkerType = MapIconType.Circle, MarkerSize = 10, Fill = Brushes.Orange, Label = "SYD" },
            new MapMarker { Latitude = 50.03, Longitude = 8.57, MarkerType = MapIconType.Triangle, MarkerSize = 12, Fill = Brushes.Green, Label = "FRA" },
        };

        CityBubbles = new[]
        {
            new CityData("Tokyo", 35.7, 139.7, 37.4),
            new CityData("Delhi", 28.6, 77.2, 32.9),
            new CityData("Shanghai", 31.2, 121.5, 29.2),
            new CityData("São Paulo", -23.5, -46.6, 22.4),
            new CityData("Mexico City", 19.4, -99.1, 21.9),
            new CityData("Cairo", 30.0, 31.2, 21.3),
            new CityData("Mumbai", 19.1, 72.9, 21.0),
            new CityData("Beijing", 39.9, 116.4, 20.9),
            new CityData("New York", 40.7, -74.0, 18.8),
            new CityData("London", 51.5, -0.1, 9.5),
            new CityData("Paris", 48.9, 2.3, 11.1),
            new CityData("Sydney", -33.9, 151.2, 5.4),
        };

        TradeRoutes = new[]
        {
            // US - Europe routes
            new RouteData(40.7, -74.0, 51.5, -0.1, 95.0),
            new RouteData(40.7, -74.0, 48.9, 2.3, 62.0),
            new RouteData(40.7, -74.0, 52.5, 13.4, 55.0),
            // US - Asia routes
            new RouteData(34.1, -118.2, 35.7, 139.7, 85.0),
            new RouteData(34.1, -118.2, 31.2, 121.5, 92.0),
            new RouteData(34.1, -118.2, 22.3, 114.2, 78.0),
            // Europe - Asia routes
            new RouteData(51.5, -0.1, 35.7, 139.7, 45.0),
            new RouteData(52.5, 13.4, 31.2, 121.5, 68.0),
            // Asia internal routes
            new RouteData(35.7, 139.7, 1.3, 103.8, 72.0),
            new RouteData(31.2, 121.5, -33.9, 151.2, 38.0),
        };

        EarthquakeData = new[]
        {
            // Pacific Ring of Fire - Japan
            new EarthquakeItem(38.3, 142.4, 9.1), // 2011 Tohoku
            new EarthquakeItem(35.0, 135.8, 6.9),
            new EarthquakeItem(34.4, 135.3, 6.1),
            // Indonesia
            new EarthquakeItem(3.3, 95.9, 9.1),   // 2004 Sumatra
            new EarthquakeItem(-0.8, 99.8, 7.6),
            new EarthquakeItem(-7.5, 110.4, 6.3),
            // Chile
            new EarthquakeItem(-36.1, -72.9, 8.8), // 2010 Chile
            new EarthquakeItem(-33.4, -70.6, 6.5),
            // California
            new EarthquakeItem(34.2, -118.4, 6.7), // Northridge
            new EarthquakeItem(37.9, -122.3, 6.9), // Loma Prieta
            new EarthquakeItem(36.2, -120.2, 5.8),
            // Alaska
            new EarthquakeItem(61.3, -149.9, 7.1),
            new EarthquakeItem(57.8, -152.4, 7.9),
            // Nepal
            new EarthquakeItem(28.2, 84.7, 7.8),  // 2015 Nepal
            // Turkey
            new EarthquakeItem(37.2, 37.0, 7.8),  // 2023 Turkey
            new EarthquakeItem(38.0, 38.5, 7.5),
            // New Zealand
            new EarthquakeItem(-41.5, 174.8, 6.3),
            new EarthquakeItem(-42.7, 173.0, 7.8),
            // Italy
            new EarthquakeItem(42.4, 13.4, 6.2),
            // Philippines
            new EarthquakeItem(15.5, 120.8, 7.7),
            // Mexico
            new EarthquakeItem(19.4, -99.4, 7.1),  // 2017 Mexico City
            // Iran
            new EarthquakeItem(33.4, 46.0, 7.3),
        };

        GdpData = new[]
        {
            new GdpItem("US", 25500.0),
            new GdpItem("CN", 18300.0),
            new GdpItem("JP", 4230.0),
            new GdpItem("DE", 4070.0),
            new GdpItem("IN", 3730.0),
            new GdpItem("GB", 3070.0),
            new GdpItem("FR", 2780.0),
            new GdpItem("IT", 2010.0),
            new GdpItem("BR", 1920.0),
            new GdpItem("CA", 2140.0),
            new GdpItem("RU", 1780.0),
            new GdpItem("KR", 1670.0),
            new GdpItem("AU", 1680.0),
            new GdpItem("MX", 1320.0),
            new GdpItem("ES", 1420.0),
        };

        FinancialCenters = new[]
        {
            new ExchangeItem("NYSE", 40.7, -74.0, 25.0),
            new ExchangeItem("NASDAQ", 40.8, -73.9, 22.0),
            new ExchangeItem("TSE", 35.7, 139.7, 6.5),
            new ExchangeItem("SSE", 31.2, 121.5, 7.4),
            new ExchangeItem("LSE", 51.5, -0.1, 3.2),
            new ExchangeItem("HKEX", 22.3, 114.2, 4.1),
        };

        InvestmentFlows = new[]
        {
            new InvestmentFlowItem(40.7, -74.0, 51.5, -0.1, 85.0),
            new InvestmentFlowItem(40.7, -74.0, 35.7, 139.7, 72.0),
            new InvestmentFlowItem(51.5, -0.1, 22.3, 114.2, 48.0),
            new InvestmentFlowItem(35.7, 139.7, 31.2, 121.5, 55.0),
        };

        InteractiveMapData = new[]
        {
            new CountryData("US", "United States", 85, 331.9, 25462.7, "North America"),
            new CountryData("BR", "Brazil", 60, 214.3, 1920.1, "South America"),
            new CountryData("CN", "China", 90, 1412.0, 17963.2, "Asia"),
            new CountryData("AU", "Australia", 40, 26.0, 1675.4, "Oceania"),
            new CountryData("ZA", "South Africa", 30, 60.0, 405.3, "Africa"),
            new CountryData("ES", "Spain", 75, 47.4, 1418.3, "Europe"),
            new CountryData("FR", "France", 70, 67.8, 2782.9, "Europe"),
            new CountryData("DE", "Germany", 80, 83.2, 4072.2, "Europe"),
            new CountryData("RU", "Russia", 55, 144.1, 1778.8, "Europe/Asia"),
            new CountryData("IN", "India", 65, 1417.2, 3385.1, "Asia"),
            new CountryData("JP", "Japan", 72, 125.7, 4231.1, "Asia"),
            new CountryData("GB", "United Kingdom", 78, 67.5, 3070.7, "Europe"),
            new CountryData("CA", "Canada", 68, 38.9, 2139.8, "North America"),
            new CountryData("MX", "Mexico", 52, 128.9, 1294.0, "North America"),
        };

        InteractiveMarkers = new ObservableCollection<MapMarker>
        {
            new MapMarker { Label = "Eiffel Tower", Latitude = 48.8584, Longitude = 2.2945 },
            new MapMarker { Label = "Statue of Liberty", Latitude = 40.6892, Longitude = -74.0445 },
            new MapMarker { Label = "Sydney Opera House", Latitude = -33.8568, Longitude = 151.2153 },
            new MapMarker { Label = "Taj Mahal", Latitude = 27.1751, Longitude = 78.0421 },
            new MapMarker { Label = "Christ the Redeemer", Latitude = -22.9519, Longitude = -43.2105 }
        };

        CitiesWithPieData = new[]
        {
            new CityPieData("New York", 40.7128, -74.0060, new ObservableCollection<PieSegment>
            {
                new PieSegment("Tech", 45),
                new PieSegment("Finance", 30),
                new PieSegment("Retail", 25)
            }),
            new CityPieData("London", 51.5074, -0.1278, new ObservableCollection<PieSegment>
            {
                new PieSegment("Tech", 30),
                new PieSegment("Finance", 50),
                new PieSegment("Retail", 20)
            }),
            new CityPieData("Tokyo", 35.6762, 139.6503, new ObservableCollection<PieSegment>
            {
                new PieSegment("Tech", 60),
                new PieSegment("Finance", 20),
                new PieSegment("Retail", 20)
            }),
            new CityPieData("Sydney", -33.8688, 151.2093, new ObservableCollection<PieSegment>
            {
                new PieSegment("Tech", 25),
                new PieSegment("Finance", 25),
                new PieSegment("Retail", 50)
            }),
            new CityPieData("Sao Paulo", -23.5505, -46.6333, new ObservableCollection<PieSegment>
            {
                new PieSegment("Tech", 20),
                new PieSegment("Finance", 30),
                new PieSegment("Retail", 50)
            })
        };

        PieChartPalette = new List<Color>
        {
            Color.Parse("#2196F3"), // Tech
            Color.Parse("#FF5722"), // Finance
            Color.Parse("#4CAF50")  // Retail
        };

        PieChartLegend = new ObservableCollection<LegendItem>
        {
            new LegendItem("Tech", new SolidColorBrush(Color.Parse("#2196F3"))),
            new LegendItem("Finance", new SolidColorBrush(Color.Parse("#FF5722"))),
            new LegendItem("Retail", new SolidColorBrush(Color.Parse("#4CAF50")))
        };
    }

    private void GenerateSeatLayout()
    {
        var features = new System.Collections.Generic.List<string>();
        SeatMapData = new ObservableCollection<SeatInfo>();

        // Layout parameters
        double seatWidth = 0.8;
        double seatHeight = 0.8;
        double seatGap = 0.2;
        double aisleWidth = 1.2;
        
        // Seat Coordinates
        // Left Block: 0, 1, 2 (start X)
        // Right Block: Start after aisle
        double leftBlockX = 0;
        double rightBlockX = 3 * (seatWidth + seatGap) + aisleWidth;
        
        double startY = 4.0; // Start after nose cone
        
        // Fuselage Geometry
        // Total Width needs to cover: LeftBlock + Aisle + RightBlock
        // LeftBlockWidth = 3 * 0.8 + 2 * 0.2 = 2.4 + 0.4 = 2.8
        // RightBlockWidth = 2.8
        // Total Width = 2.8 + 1.2 + 2.8 = 6.8
        // Center X = 3.4
        double centerX = leftBlockX + (3 * (seatWidth + seatGap) + aisleWidth + 3 * (seatWidth + seatGap)) / 2.0 - 0.5; // Approx center
        centerX = 3.4; 
        
        double fuselageWidth = 8.8;
        double fL = centerX - fuselageWidth / 2.0;
        double fR = centerX + fuselageWidth / 2.0;
        double fTop = 0;
        double fBottom = 0; // Calculated later

        int businessRows = 3;
        int economyRows = 8;
        
        // Generate Seats
        double currentY = startY;

        // Business Class (2-2)
        // A _ C | D _ F  (Gap for comfort)
        for (int row = 1; row <= businessRows; row++)
        {
            // Left (use pos 0 and 2)
            AddRoundedSeat(features, row, "A", leftBlockX, currentY, seatWidth, seatHeight, "Business", 500);
            AddRoundedSeat(features, row, "B", leftBlockX + 1.5 * (seatWidth + seatGap), currentY, seatWidth, seatHeight, "Business", 500); // Widen gap

            // Right
            AddRoundedSeat(features, row, "E", rightBlockX, currentY, seatWidth, seatHeight, "Business", 500);
            AddRoundedSeat(features, row, "F", rightBlockX + 1.5 * (seatWidth + seatGap), currentY, seatWidth, seatHeight, "Business", 500);
            
            currentY += seatHeight + 0.8; // More legroom
        }

        currentY += 1.0; // Galley/Separator gap

        // Economy Class (3-3)
        // A B C | D E F
        for (int row = businessRows + 1; row <= businessRows + economyRows; row++)
        {
            // Left
            AddRoundedSeat(features, row, "A", leftBlockX, currentY, seatWidth, seatHeight, "Economy", 150);
            AddRoundedSeat(features, row, "B", leftBlockX + 1 * (seatWidth + seatGap), currentY, seatWidth, seatHeight, "Economy", 150);
            AddRoundedSeat(features, row, "C", leftBlockX + 2 * (seatWidth + seatGap), currentY, seatWidth, seatHeight, "Economy", 150);

            // Right
            AddRoundedSeat(features, row, "D", rightBlockX, currentY, seatWidth, seatHeight, "Economy", 150);
            AddRoundedSeat(features, row, "E", rightBlockX + 1 * (seatWidth + seatGap), currentY, seatWidth, seatHeight, "Economy", 150);
            AddRoundedSeat(features, row, "F", rightBlockX + 2 * (seatWidth + seatGap), currentY, seatWidth, seatHeight, "Economy", 150);
            
            currentY += seatHeight + 0.4;
        }
        
        fBottom = currentY + 4.0;

        // Generate Fuselage Shape (Nose Cone + Body + Tail)
        var fuselagePoints = new System.Text.StringBuilder();
        var culture = System.Globalization.CultureInfo.InvariantCulture;

        // Start Bottom Left
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fL + 2, fBottom); // Tail taper start
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fL, fBottom - 4); // Body start
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fL, startY); // Body straight
        
        // Nose Curve (Quadratic Bezier approx or just points)
        // Left Nose
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fL + 0.5, fTop + 2.5);
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", centerX - 1.5, fTop + 0.5);
        // Nose Tip
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", centerX, fTop);
        // Right Nose
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", centerX + 1.5, fTop + 0.5);
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fR - 0.5, fTop + 2.5);

        // Body Right
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fR, startY);
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fR, fBottom - 4);
        
        // Tail
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", fR - 2, fBottom);
        fuselagePoints.AppendFormat(culture, "[{0},{1}],", centerX, fBottom + 2); // Tail tip
        
        // Close Loop
        fuselagePoints.AppendFormat(culture, "[{0},{1}]", fL + 2, fBottom);

        string fuselageFeature = $@"{{
      ""type"": ""Feature"",
      ""properties"": {{
        ""id"": ""fuselage"",
        ""class"": ""Fuselage""
      }},
      ""geometry"": {{
        ""type"": ""Polygon"",
        ""coordinates"": [[{fuselagePoints.ToString()}]]
      }},
      ""id"": ""fuselage""
    }}";
        
        // Wrap fuselage in FeatureCollection
        FuselageGeoJson = $@"{{
  ""type"": ""FeatureCollection"",
  ""features"": [
    {fuselageFeature}
  ]
}}";

        // Wrap seats in FeatureCollection
        SeatMapGeoJson = $@"{{
  ""type"": ""FeatureCollection"",
  ""features"": [
    {string.Join(",\n", features)}
  ]
}}";
    }

    private void AddRoundedSeat(System.Collections.Generic.List<string> features, int row, string letter, double x, double y, double w, double h, string seatClass, double price)
    {
        string seatId = $"{row}{letter}";
        double r = 0.2; // Corner radius
        var culture = System.Globalization.CultureInfo.InvariantCulture;

        // Generate rounded rect path
        // TL, TR, BR, BL
        string coordinates = string.Format(culture, 
            "[[[{0},{1}], [{2},{3}], [{4},{3}], [{5},{1}], [{5},{6}], [{4},{7}], [{2},{7}], [{0},{6}], [{0},{1}]]]",
            x, y + r, // Start (TL vertical)
            x, y,     // TL Corner pt 1 (Should be arc, simple chamfer for now)
            x + r, y, // TL Top
            
            x + w - r, // TR coords... simplified chamfer box for readability/code size
            x + w, 
            y + h - r,
            y + h
        );
            
        // Proper rounded rect with 8 points (chamfered)
        // P1(x, y+r), P2(x+r, y), P3(x+w-r, y), P4(x+w, y+r), P5(x+w, y+h-r), P6(x+w-r, y+h), P7(x+r, y+h), P8(x, y+h-r)
        
        var sb = new System.Text.StringBuilder();
        sb.Append("[[");
        sb.AppendFormat(culture, "[{0},{1}],", x, y + r);
        sb.AppendFormat(culture, "[{0},{1}],", x + r, y);
        sb.AppendFormat(culture, "[{0},{1}],", x + w - r, y);
        sb.AppendFormat(culture, "[{0},{1}],", x + w, y + r);
        sb.AppendFormat(culture, "[{0},{1}],", x + w, y + h - r);
        sb.AppendFormat(culture, "[{0},{1}],", x + w - r, y + h);
        sb.AppendFormat(culture, "[{0},{1}],", x + r, y + h);
        sb.AppendFormat(culture, "[{0},{1}],", x, y + h - r);
        sb.AppendFormat(culture, "[{0},{1}]", x, y + r); // Close
        sb.Append("]]");

        string feature = $@"{{
      ""type"": ""Feature"",
      ""properties"": {{
        ""id"": ""{seatId}"",
        ""class"": ""{seatClass}""
      }},
      ""geometry"": {{
        ""type"": ""Polygon"",
        ""coordinates"": {sb.ToString()}
      }},
      ""id"": ""{seatId}""
    }}";

        features.Add(feature);
        SeatMapData.Add(new SeatInfo(seatId, seatClass, price, "Available"));
    }
    }
