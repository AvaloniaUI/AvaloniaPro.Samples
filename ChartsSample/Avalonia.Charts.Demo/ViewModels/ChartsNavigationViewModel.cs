using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Charts.Demo.ViewModels.Applications;
using Avalonia.Charts.Demo.Views;
using Avalonia.Charts.Demo.Views.Applications;
using Avalonia.Charts.Demo.Views.Charts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Charts.Demo.ViewModels;

public sealed partial class ChartsNavigationViewModel : ViewModelBase
{
    private NavigationItemViewModel? _selectedItem;

    [ObservableProperty] private UserControl? _currentContent;

    public ChartsNavigationViewModel()
    {
        Sections = new ObservableCollection<NavigationSectionViewModel>
        {
            CreateSection(null, false,
                Item("Home", () => new ChartsWelcomePage())),

            CreateSection("Axis", true,
                Item("Types", () => new AxisTypesPage()),
                Item("Title", () => new AxisTitlePage()),
                Item("Gridlines", () => new GridlinesPage()),
                Item("Customization", () => new AxisCustomizationPage()),
                Item("Dual Axis", () => new DualAxisPage()),
                Item("Scale Breaks", () => new ScaleBreaksPage()),
                Item("Plot Bands", () => new PlotBandsPage()),
                Item("Smart Labels", () => new SmartLabelsPage()),
                Item("Continuous Axis", () => new ContinuousXAxisPage())),

            CreateSection("Chart Types", true,
                Item("Cartesian", () => new CartesianSeriesPage()),
                Item("Pie & Donut", () => new PieChartsPage()),
                Item("Bubble", () => new BubbleChartsPage()),
                Item("Gauges", () => new GaugeChartsPage()),
                Item("Radial", () => new RadialChartsPage()),
                Item("Financial", () => new FinancialChartsPage()),
                Item("Hierarchical", () => new HierarchicalChartsPage()),
                Item("Flow & Network", () => new FlowChartsPage()),
                Item("Timeline", () => new TimelineChartsPage()),
                Item("Statistical", () => new StatisticalChartsPage()),
                Item("Analytics", () => new AnalyticsChartsPage()),
                Item("Engineering", () => new EngineeringChartsPage()),
                Item("Map", () => new MapChartsPage()),
                Item("Multiple/Combo", () => new MultipleChartsPage())),

            CreateSection(null, true,
                Item("Animation", () => new AnimationPage()),
                Item("Appearance", () => new AppearanceChartsPage()),
                Item("Item Customization", () => new ItemCustomizationPage()),
                Item("Data Path", () => new DataPathPage()),
                Item("Empty Points", () => new EmptyPointsPage()),
                Item("Markers", () => new MarkersPage()),
                Item("Data Labels", () => new DataLabelsPage()),
                Item("Tooltips", () => new TooltipsPage()),
                Item("Crosshairs", () => new CrosshairsPage()),
                Item("Trackball", () => new TrackballPage()),
                Item("Highlight", () => new HighlightPage()),
                Item("Selection", () => new SelectionPage()),
                Item("Zoom / Pan", () => new ZoomingPage()),
                Item("Annotations", () => new AnnotationsPage()),
                Item("Trendlines", () => new TrendlinesPage()),
                Item("Technical Indicators", () => new TechnicalIndicatorsPage()),
                Item("Legend", () => new LegendChartsPage()),
                Item("Exporting", () => new ExportingPage()),
                Item("Live Data Updates", () => new LiveDataUpdatesPage()),
                Item("Real-Time", () => new RealTimeChartsPage()),
                Item("Benchmark", () => new BenchmarkChartsPage())),

            CreateSection("A11y", true,
                Item("Patterns", () => new AccessibilityPage())),

#if DEBUG
            CreateSection("Debug", true,
                Item("Diagnostics", () => new DiagnosticsPage())),
#endif

            CreateSection("Applications", true,
                Item("Crypto Dashboard", () => new CryptoDashboardPage { DataContext = new CryptoDashboardViewModel() }),
                Item("Sales Dashboard", () => new SalesDashboardPage { DataContext = new SalesDashboardViewModel() }))
        };

        Sections
            .SelectMany(section => section.Items)
            .First(item => item.Title == "Home")
            .SelectCommand.Execute(null);
    }

    public ObservableCollection<NavigationSectionViewModel> Sections { get; }

    internal void SelectItem(NavigationItemViewModel item)
    {
        if (ReferenceEquals(_selectedItem, item))
        {
            CurrentContent = item.GetOrCreateContent();
            return;
        }

        if (_selectedItem != null)
        {
            _selectedItem.IsSelected = false;
        }

        _selectedItem = item;
        _selectedItem.IsSelected = true;
        CurrentContent = item.GetOrCreateContent();
    }

    private NavigationSectionViewModel CreateSection(string? header, bool showSeparator,
        params (string title, Func<UserControl> factory)[] items)
    {
        return new NavigationSectionViewModel(
            header,
            showSeparator,
            items.Select(item => new NavigationItemViewModel(this, item.title, item.factory)));
    }

    private static (string title, Func<UserControl> factory) Item(string title, Func<UserControl> factory) =>
        (title, factory);
}

public sealed class NavigationSectionViewModel
{
    public NavigationSectionViewModel(string? header, bool showSeparator, IEnumerable<NavigationItemViewModel> items)
    {
        Header = header;
        ShowSeparator = showSeparator;
        Items = new ObservableCollection<NavigationItemViewModel>(items);
    }

    public string? Header { get; }
    public bool ShowSeparator { get; }
    public bool HasHeader => !string.IsNullOrWhiteSpace(Header);
    public ObservableCollection<NavigationItemViewModel> Items { get; }
}

public sealed partial class NavigationItemViewModel : ViewModelBase
{
    private readonly ChartsNavigationViewModel _owner;
    private readonly Func<UserControl> _contentFactory;
    private UserControl? _cachedContent;

    [ObservableProperty] private bool _isSelected;

    public NavigationItemViewModel(ChartsNavigationViewModel owner, string title, Func<UserControl> contentFactory)
    {
        _owner = owner;
        _contentFactory = contentFactory;
        Title = title;
    }

    public string Title { get; }

    [RelayCommand]
    private void Select()
    {
        _owner.SelectItem(this);
    }

    internal UserControl GetOrCreateContent()
    {
        _cachedContent ??= _contentFactory();
        return _cachedContent;
    }
}
