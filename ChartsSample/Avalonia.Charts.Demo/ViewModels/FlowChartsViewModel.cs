using System.Collections.Generic;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Charts;
using Avalonia.Media.Immutable;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class FlowChartsViewModel : ViewModelBase
{
    [ObservableProperty]
    private List<FlowNode> _flowNodes = new();

    [ObservableProperty]
    private List<FlowEdge> _flowEdges = new();

    [ObservableProperty]
    private object[] _sankeyData = System.Array.Empty<object>();

    [ObservableProperty]
    private object[] _chordData = System.Array.Empty<object>();

    [ObservableProperty]
    private object[] _arcNodes = System.Array.Empty<object>();

    [ObservableProperty]
    private object[] _arcLinks = System.Array.Empty<object>();

    [ObservableProperty]
    private object[] _forceNodes = System.Array.Empty<object>();

    [ObservableProperty]
    private object[] _forceEdges = System.Array.Empty<object>();

    [ObservableProperty]
    private NetworkNode[] _networkNodes = System.Array.Empty<NetworkNode>();

    [ObservableProperty]
    private NetworkEdge[] _networkEdges = System.Array.Empty<NetworkEdge>();

    [ObservableProperty]
    private AlluvialNode[] _alluvialNodes = System.Array.Empty<AlluvialNode>();

    [ObservableProperty]
    private AlluvialLink[] _alluvialLinks = System.Array.Empty<AlluvialLink>();

    [ObservableProperty]
    private List<FlowNode> _archNodes = new();

    [ObservableProperty]
    private List<FlowEdge> _archEdges = new();

    [ObservableProperty]
    private List<FlowGroup> _archGroups = new();

    [ObservableProperty]
    private List<FlowNode> _mindmapNodes = new();

    [ObservableProperty]
    private List<FlowEdge> _mindmapEdges = new();

    [ObservableProperty]
    private List<FlowNode> _timelineNodes = new();

    [ObservableProperty]
    private List<FlowEdge> _timelineEdges = new();

    [ObservableProperty]
    private List<FlowGroup> _timelineGroups = new();

    public FlowChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Process Flow Chart
        FlowNodes = new List<FlowNode>
        {
            new FlowNode
            {
                Id = "start",
                Text = "Lamp doesn't work",
                Shape = FlowShape.RoundedRect,
                X = 300, Y = 50,
                Background = Brushes.Salmon,
                Foreground = Brushes.White,
                Width = 200
            },
            new FlowNode
            {
                Id = "check_plug",
                Text = "Is lamp plugged in?",
                Shape = FlowShape.Diamond,
                X = 280, Y = 150,
                Width = 240, Height = 100,
                Background = Brushes.LightBlue
            },
            new FlowNode
            {
                Id = "plug_in",
                Text = "Plug in lamp",
                Shape = FlowShape.Rectangle,
                X = 550, Y = 170,
                Background = Brushes.LightGreen
            },
            new FlowNode
            {
                Id = "check_bulb",
                Text = "Is bulb burned out?",
                Shape = FlowShape.Diamond,
                X = 280, Y = 300,
                Width = 240, Height = 100,
                Background = Brushes.LightBlue
            },
            new FlowNode
            {
                Id = "replace_bulb",
                Text = "Replace bulb",
                Shape = FlowShape.Rectangle,
                X = 550, Y = 320,
                Background = Brushes.LightGreen
            },
                new FlowNode
            {
                Id = "repair",
                Text = "Buy new lamp",
                Shape = FlowShape.Rectangle,
                X = 300, Y = 450,
                Background = Brushes.LightGreen
            },
        };

        FlowEdges = new List<FlowEdge>
        {
            new FlowEdge { SourceId = "start", TargetId = "check_plug" },
            new FlowEdge { SourceId = "check_plug", TargetId = "plug_in", Label = "No" },
            new FlowEdge { SourceId = "check_plug", TargetId = "check_bulb", Label = "Yes" },
            new FlowEdge { SourceId = "check_bulb", TargetId = "replace_bulb", Label = "Yes" },
            new FlowEdge { SourceId = "check_bulb", TargetId = "repair", Label = "No" },
        };

        // Sankey Chart
        SankeyData = new[]
        {
            new { Source = "Solar", Target = "Grid", Value = 40.0 },
            new { Source = "Wind", Target = "Grid", Value = 30.0 },
            new { Source = "Coal", Target = "Grid", Value = 10.0 },
            new { Source = "Grid", Target = "Industry", Value = 30.0 },
            new { Source = "Grid", Target = "Residential", Value = 25.0 },
            new { Source = "Grid", Target = "Transport", Value = 15.0 },
            new { Source = "Grid", Target = "Losses", Value = 10.0 },
        };

        // Chord Diagram
        ChordData = new[]
        {
            new { Source = "USA", Target = "China", Value = 50.0 },
            new { Source = "USA", Target = "Europe", Value = 40.0 },
            new { Source = "Europe", Target = "China", Value = 30.0 },
            new { Source = "China", Target = "USA", Value = 45.0 },
            new { Source = "Europe", Target = "USA", Value = 35.0 },
        };

        // Arc Diagram
        ArcNodes = new[]
        {
            new { Id = "1", Label = "Chapter 1" },
            new { Id = "2", Label = "Chapter 2" },
            new { Id = "3", Label = "Chapter 3" },
            new { Id = "4", Label = "Chapter 4" },
            new { Id = "5", Label = "Chapter 5" },
        };

        ArcLinks = new[]
        {
            new { Source = "1", Target = "2", Value = 1.0 },
            new { Source = "1", Target = "3", Value = 3.0 },
            new { Source = "2", Target = "4", Value = 2.0 },
            new { Source = "3", Target = "5", Value = 4.0 },
            new { Source = "2", Target = "5", Value = 1.0 },
        };

        // Force-Directed Graph
        ForceNodes = new[]
        {
            new { Id = "N1", Label = "Main" },
            new { Id = "N2", Label = "Server 1" },
            new { Id = "N3", Label = "Server 2" },
            new { Id = "N4", Label = "Client A" },
            new { Id = "N5", Label = "Client B" },
            new { Id = "N6", Label = "DB" },
        };

        ForceEdges = new[]
        {
            new { Source = "N1", Target = "N2" },
            new { Source = "N1", Target = "N3" },
            new { Source = "N2", Target = "N6" },
            new { Source = "N3", Target = "N6" },
            new { Source = "N2", Target = "N4" },
            new { Source = "N3", Target = "N5" },
        };

        // Network Chart
        NetworkNodes = new[]
        {
            new NetworkNode { Id = "A", Label = "Alice", X = 0, Y = 0 },
            new NetworkNode { Id = "B", Label = "Bob", X = 0, Y = 0 },
            new NetworkNode { Id = "C", Label = "Charlie", X = 0, Y = 0 },
            new NetworkNode { Id = "D", Label = "David", X = 0, Y = 0 },
            new NetworkNode { Id = "E", Label = "Eve", X = 0, Y = 0 },
        };

        NetworkEdges = new[]
        {
            new NetworkEdge { Source = "A", Target = "B", Weight = 2 },
            new NetworkEdge { Source = "A", Target = "C", Weight = 1 },
            new NetworkEdge { Source = "B", Target = "D", Weight = 3 },
            new NetworkEdge { Source = "C", Target = "E", Weight = 1 },
            new NetworkEdge { Source = "D", Target = "E", Weight = 2 },
            new NetworkEdge { Source = "B", Target = "E", Weight = 1 },
        };

        // Alluvial Chart
        AlluvialNodes = new[]
        {
            new AlluvialNode { Id = "Jan", Label = "Jan", Step = 0, Value = 50 },
            new AlluvialNode { Id = "Feb", Label = "Feb", Step = 0, Value = 60 },
            new AlluvialNode { Id = "CatA", Label = "Category A", Step = 1, Value = 55 },
            new AlluvialNode { Id = "CatB", Label = "Category B", Step = 1, Value = 55 },
        };

        AlluvialLinks = new[]
        {
            new AlluvialLink { Source = "Jan", Target = "CatA", Value = 30 },
            new AlluvialLink { Source = "Jan", Target = "CatB", Value = 20 },
            new AlluvialLink { Source = "Feb", Target = "CatA", Value = 25 },
            new AlluvialLink { Source = "Feb", Target = "CatB", Value = 35 },
        };

        InitializeArchitectureData();

        InitializeMindmapData();

        InitializeTimelineData();

        GenerateThemeRiverData();
    }

    [ObservableProperty]
    private List<ThemeRiverItem> _themeRiverDummy = new();
    [ObservableProperty]
    private List<ThemeRiverItem> _themeRiverSeriesA = new();
    [ObservableProperty]
    private List<ThemeRiverItem> _themeRiverSeriesB = new();
    [ObservableProperty]
    private List<ThemeRiverItem> _themeRiverSeriesC = new();
    [ObservableProperty]
    private List<StreamGraphItem> _streamGraphSeries = new();

    private void GenerateThemeRiverData()
    {
        var dummy = new List<ThemeRiverItem>();
        var seriesA = new List<ThemeRiverItem>();
        var seriesB = new List<ThemeRiverItem>();
        var seriesC = new List<ThemeRiverItem>();
        var streamGraph = new List<StreamGraphItem>();

        var count = 30;
        var center = 50.0;

        for (int i = 0; i < count; i++)
        {
            var date = $"T{i}";
            
            // Generate values with sine waves
            var valA = 10 + 5 * System.Math.Sin(i * 0.3);
            var valB = 15 + 8 * System.Math.Sin(i * 0.5 + 1);
            var valC = 12 + 6 * System.Math.Cos(i * 0.4);

            var totalHeight = valA + valB + valC;
            var offset = center - (totalHeight / 2);
            var streamValue = 24 + 8 * System.Math.Sin(i * 0.35) + 5 * System.Math.Cos(i * 0.18 + 0.8);
            var color = i switch
            {
                < 8 => new ImmutableSolidColorBrush(Color.Parse("#4E79A7")),
                < 15 => new ImmutableSolidColorBrush(Color.Parse("#59A14F")),
                < 22 => new ImmutableSolidColorBrush(Color.Parse("#F28E2B")),
                _ => new ImmutableSolidColorBrush(Color.Parse("#E15759"))
            };

            dummy.Add(new ThemeRiverItem { Category = date, Value = offset });
            seriesA.Add(new ThemeRiverItem { Category = date, Value = valA });
            seriesB.Add(new ThemeRiverItem { Category = date, Value = valB });
            seriesC.Add(new ThemeRiverItem { Category = date, Value = valC });
            streamGraph.Add(new StreamGraphItem { Category = date, Value = streamValue, Color = color });
        }

        ThemeRiverDummy = dummy;
        ThemeRiverSeriesA = seriesA;
        ThemeRiverSeriesB = seriesB;
        ThemeRiverSeriesC = seriesC;
        StreamGraphSeries = streamGraph;
    }

    private void InitializeArchitectureData()
    {
        // SVG paths for common architecture icons
        const string cloudIcon = "M19.35 10.04C18.67 6.59 15.64 4 12 4 9.11 4 6.6 5.64 5.35 8.04 2.34 8.36 0 10.91 0 14c0 3.31 2.69 6 6 6h13c2.76 0 5-2.24 5-5 0-2.64-2.05-4.78-4.65-4.96z";
        const string serverIcon = "M2 20h20v-4H2v4zm2-3h2v2H4v-2zM2 4v4h20V4H2zm4 3H4V5h2v2zm-4 7h20v-4H2v4zm2-3h2v2H4v-2z";
        const string dbIcon = "M12 2C8.13 2 5 3.79 5 6s3.13 4 7 4 7-1.79 7-4-3.13-4-7-4zm0 10c-3.87 0-7-1.79-7-4v3c0 2.21 3.13 4 7 4s7-1.79 7-4V8c0 2.21-3.13 4-7 4zm0 4c-3.87 0-7-1.79-7-4v3c0 2.21 3.13 4 7 4s7-1.79 7-4v-3c0 2.21-3.13 4-7 4z";

        ArchNodes = new List<FlowNode>
        {
            // Cloud Group
            new FlowNode { Id = "lb", Text = "Load Balancer", X = 150, Y = 170, Shape = FlowShape.Icon, Icon = cloudIcon, Background = Brushes.SkyBlue, Width = 80, Height = 80 },
            new FlowNode { Id = "web1", Text = "Web Server 1", X = 350, Y = 90, Shape = FlowShape.Icon, Icon = serverIcon, Background = Brushes.LightGray, Width = 70, Height = 70 },
            new FlowNode { Id = "web2", Text = "Web Server 2", X = 350, Y = 250, Shape = FlowShape.Icon, Icon = serverIcon, Background = Brushes.LightGray, Width = 70, Height = 70 },
            
            // On-Premise Group
            new FlowNode { Id = "db", Text = "PostgreSQL", X = 600, Y = 170, Shape = FlowShape.Icon, Icon = dbIcon, Background = Brushes.Orange, Width = 80, Height = 80 }
        };

        ArchEdges = new List<FlowEdge>
        {
            new FlowEdge { SourceId = "lb", TargetId = "web1", Label = "HTTP" },
            new FlowEdge { SourceId = "lb", TargetId = "web2", Label = "HTTP" },
            new FlowEdge { SourceId = "web1", TargetId = "db", Label = "SQL" },
            new FlowEdge { SourceId = "web2", TargetId = "db", Label = "SQL" }
        };

        ArchGroups = new List<FlowGroup>
        {
            new FlowGroup 
            { 
                Id = "g1", Label = "Public Cloud", 
                NodeIds = new[] { "lb", "web1", "web2" },
                Background = new SolidColorBrush(Colors.Blue, 0.05),
                BorderBrush = Brushes.CornflowerBlue,
                BorderThickness = 1
            },
            new FlowGroup
            {
                Id = "g2", Label = "On-Premise",
                NodeIds = new[] { "db" },
                Background = new SolidColorBrush(Colors.Orange, 0.05),
                BorderBrush = Brushes.Orange,
                BorderThickness = 1
            }
        };
    }

    private void InitializeMindmapData()
    {
        MindmapNodes = new List<FlowNode>
        {
            // Root
            new FlowNode { Id = "root", Text = "Project Alpha", X = 350, Y = 250, Shape = FlowShape.Circle, Background = Brushes.MediumPurple, Foreground = Brushes.White, Width = 120, Height = 120 },
            
            // Branch 1: Research
            new FlowNode { Id = "res", Text = "Research", X = 200, Y = 150, Shape = FlowShape.RoundedRect, Background = Brushes.Salmon, Width = 100 },
            new FlowNode { Id = "re1", Text = "User Specs", X = 50, Y = 100, Shape = FlowShape.Rectangle, Background = Brushes.Snow, Width = 100 },
            new FlowNode { Id = "re2", Text = "Competitors", X = 50, Y = 200, Shape = FlowShape.Rectangle, Background = Brushes.Snow, Width = 100 },
            
            // Branch 2: Design
            new FlowNode { Id = "des", Text = "Design", X = 500, Y = 150, Shape = FlowShape.RoundedRect, Background = Brushes.SkyBlue, Width = 100 },
            new FlowNode { Id = "de1", Text = "UI/UX", X = 650, Y = 100, Shape = FlowShape.Rectangle, Background = Brushes.Snow, Width = 100 },
            new FlowNode { Id = "de2", Text = "Architecture", X = 650, Y = 200, Shape = FlowShape.Rectangle, Background = Brushes.Snow, Width = 100 },
            
            // Branch 3: Implementation
            new FlowNode { Id = "imp", Text = "Implementation", X = 350, Y = 400, Shape = FlowShape.RoundedRect, Background = Brushes.LightGreen, Width = 120 },
            new FlowNode { Id = "im1", Text = "Frontend", X = 250, Y = 500, Shape = FlowShape.Rectangle, Background = Brushes.Snow, Width = 100 },
            new FlowNode { Id = "im2", Text = "Backend", X = 450, Y = 500, Shape = FlowShape.Rectangle, Background = Brushes.Snow, Width = 100 }
        };

        MindmapEdges = new List<FlowEdge>
        {
            new FlowEdge { SourceId = "root", TargetId = "res", ShowArrow = false },
            new FlowEdge { SourceId = "res", TargetId = "re1", ShowArrow = false },
            new FlowEdge { SourceId = "res", TargetId = "re2", ShowArrow = false },
            
            new FlowEdge { SourceId = "root", TargetId = "des", ShowArrow = false },
            new FlowEdge { SourceId = "des", TargetId = "de1", ShowArrow = false },
            new FlowEdge { SourceId = "des", TargetId = "de2", ShowArrow = false },
            
            new FlowEdge { SourceId = "root", TargetId = "imp", ShowArrow = false },
            new FlowEdge { SourceId = "imp", TargetId = "im1", ShowArrow = false },
            new FlowEdge { SourceId = "imp", TargetId = "im2", ShowArrow = false }
        };
    }

    private void InitializeTimelineData()
    {
        // Timeline: History of Social Media Platform 
        TimelineNodes = new List<FlowNode>();
        TimelineEdges = new List<FlowEdge>();
        TimelineGroups = new List<FlowGroup>(); 

        double startX = 100;
        double gap = 200;
        double axisY = 200;

        // 1. Central Axis Line
        // We create invisible anchor nodes to define the line segments
        
        var axisColor = Brushes.DarkSlateGray;

        // Visual Data
        var events = new List<(string Year, string Title, Color Color)>
        {
            ("2002", "LinkedIn", Colors.MediumSlateBlue),
            ("2004", "Facebook", Colors.Khaki),
            ("2005", "YouTube", Colors.LightGreen),
            ("2006", "Twitter", Colors.MediumOrchid)
        };

        // Create Nodes
        for (int i = 0; i < events.Count; i++)
        {
            var (year, title, color) = events[i];
            double x = startX + i * gap;
            
            // 1. Year Header (Top)
            var yearNode = new FlowNode
            {
                Id = $"y_{year}",
                Text = year,
                X = x - 60,
                Y = axisY - 120,
                Width = 120,
                Height = 40,
                Shape = FlowShape.RoundedRect,
                Background = new SolidColorBrush(color) { Opacity = 0.6 },
                Foreground = Brushes.Black
            };
            TimelineNodes.Add(yearNode);

            // 2. Event Node (Bottom)
            var eventNode = new FlowNode
            {
                Id = $"e_{title}",
                Text = title,
                X = x - 60,
                Y = axisY + 80, // Below axis
                Width = 120,
                Height = 40,
                Shape = FlowShape.RoundedRect,
                Background = new SolidColorBrush(color) { Opacity = 0.6 },
                Foreground = Brushes.Black
            };
            TimelineNodes.Add(eventNode);

            // 3. Axis Anchor Node (Invisible/Small)
            var anchorId = $"anc_{i}";
            var anchorNode = new FlowNode 
            { 
                Id = anchorId, 
                X = x, 
                Y = axisY, 
                Width = 1, 
                Height = 1, 
                Shape = FlowShape.Circle,
                Background = Brushes.Transparent 
            };
            TimelineNodes.Add(anchorNode);

            // Edges: Year -> Anchor
            TimelineEdges.Add(new FlowEdge 
            { 
                SourceId = yearNode.Id, 
                TargetId = anchorId, 
                ShowArrow = false 
            });

            // Edges: Anchor -> Event
            TimelineEdges.Add(new FlowEdge 
            { 
                SourceId = anchorId, 
                TargetId = eventNode.Id, 
                ShowArrow = true
            });
        }

        // Add Main Axis Line (Solid, connecting anchors)
        for (int i = 0; i < events.Count - 1; i++)
        {
             TimelineEdges.Add(new FlowEdge 
            { 
                SourceId = $"anc_{i}", 
                TargetId = $"anc_{i+1}", 
                ShowArrow = false
            });
        }
        
        // Extend axis start and end
        var startNode = new FlowNode { Id = "axis_start", X = startX - 100, Y = axisY, Width = 1, Height = 1, Background=Brushes.Transparent };
        var endNode = new FlowNode { Id = "axis_end", X = startX + (events.Count - 1) * gap + 100, Y = axisY, Width = 1, Height = 1, Background=Brushes.Transparent };
        TimelineNodes.Add(startNode);
        TimelineNodes.Add(endNode);

        // Connect start to first anchor
        TimelineEdges.Insert(0, new FlowEdge { SourceId = "axis_start", TargetId = "anc_0", ShowArrow = false });
        // Connect last anchor to end
        TimelineEdges.Add(new FlowEdge { SourceId = $"anc_{events.Count-1}", TargetId = "axis_end", ShowArrow = true });
    }
}

public class ThemeRiverItem
{
    public string Category { get; set; } = string.Empty;
    public double Value { get; set; }
}

public class StreamGraphItem
{
    public string Category { get; set; } = string.Empty;
    public double Value { get; set; }
    public IBrush? Color { get; set; }
}
