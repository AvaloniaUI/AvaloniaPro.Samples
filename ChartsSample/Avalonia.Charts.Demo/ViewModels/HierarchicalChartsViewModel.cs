using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class HierarchicalChartsViewModel : ViewModelBase
{
    public HierarchicalChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // TreeMap Data
        TreeMapData = new ObservableCollection<TreeMapItem>
        {
            new("Documents", 45.0),
            new("Photos", 30.0),
            new("Videos", 50.0),
            new("Music", 20.0),
            new("Downloads", 35.0)
        };

        // Sunburst Data
        SunburstData = new ObservableCollection<SunburstNode>
        {
            new SunburstNode
            {
                Name = "Engineering", Size = 40.0, Children = new List<SunburstNode>
                {
                    new SunburstNode
                    {
                        Name = "Frontend", Size = 15.0, Children = new List<SunburstNode>
                        {
                            new SunburstNode { Name = "React", Size = 8.0 },
                            new SunburstNode { Name = "Angular", Size = 7.0 }
                        }
                    },
                    new SunburstNode { Name = "Backend", Size = 18.0 },
                    new SunburstNode { Name = "DevOps", Size = 7.0 }
                }
            },
            new SunburstNode
            {
                Name = "Sales", Size = 30.0, Children = new List<SunburstNode>
                {
                    new SunburstNode { Name = "Enterprise", Size = 18.0 },
                    new SunburstNode { Name = "SMB", Size = 12.0 }
                }
            },
            new SunburstNode
            {
                Name = "Marketing", Size = 20.0, Children = new List<SunburstNode>
                {
                    new SunburstNode { Name = "Digital", Size = 12.0 },
                    new SunburstNode { Name = "Brand", Size = 8.0 }
                }
            },
            new SunburstNode { Name = "HR", Size = 10.0 }
        };

        // Icicle Data
        IcicleData = new ObservableCollection<TreeMapItem>
        {
            new("src", 60.0),
            new("tests", 25.0),
            new("docs", 15.0)
        };

        // Circle Packing Data
        CirclePackingData = new ObservableCollection<TreeMapItem>
        {
            new("Core", 40.0),
            new("Utils", 25.0),
            new("UI", 35.0)
        };

        // Organization Chart Data
        var ceo = new OrgNode { Name = "CEO", Reports = new List<OrgNode>() };
        ceo.Reports.Add(new OrgNode { Name = "CTO", Reports = new List<OrgNode> { new OrgNode { Name = "Dev Lead" } } });
        ceo.Reports.Add(new OrgNode { Name = "CFO", Reports = new List<OrgNode> { new OrgNode { Name = "Accounting" } } });
        OrgChartData = new ObservableCollection<OrgNode> { ceo };

        // Dendrogram Data
        var dendroRoot = new TreeNode { Name = "Root", Children = new List<TreeNode>() };
        dendroRoot.Children.Add(new TreeNode { Name = "A", Children = new List<TreeNode> { new TreeNode { Name = "A1" }, new TreeNode { Name = "A2" } } });
        dendroRoot.Children.Add(new TreeNode { Name = "B", Children = new List<TreeNode> { new TreeNode { Name = "B1" } } });
        DendrogramData = new ObservableCollection<TreeNode> { dendroRoot };

        // Indented Tree Data
        var indentedRoot = new TreeNode { Name = "src", Children = new List<TreeNode>() };
        indentedRoot.Children.Add(new TreeNode { Name = "components", Children = new List<TreeNode> { new TreeNode { Name = "Button.cs" }, new TreeNode { Name = "Chart.cs" } } });
        indentedRoot.Children.Add(new TreeNode { Name = "models", Children = new List<TreeNode> { new TreeNode { Name = "User.cs" } } });
        indentedRoot.Children.Add(new TreeNode { Name = "Program.cs" });
        IndentedTreeData = new ObservableCollection<TreeNode> { indentedRoot };

        // Radial Tree Data
        var radialRoot = new TreeNode { Name = "Animals", Children = new List<TreeNode>() };
        radialRoot.Children.Add(new TreeNode { Name = "Mammals", Children = new List<TreeNode> { new TreeNode { Name = "Dog" }, new TreeNode { Name = "Cat" } } });
        radialRoot.Children.Add(new TreeNode { Name = "Birds", Children = new List<TreeNode> { new TreeNode { Name = "Eagle" } } });
        radialRoot.Children.Add(new TreeNode { Name = "Fish", Children = new List<TreeNode> { new TreeNode { Name = "Salmon" } } });
        RadialTreeData = new ObservableCollection<TreeNode> { radialRoot };

        // Flame Graph Data (Stack Trace)
        StackTraceData = new ObservableCollection<StackItem>
        {
            new StackItem("Program.Main", 2000, new List<StackItem>
            {
                new StackItem("App.OnFrameworkInitializationCompleted", 1950, new List<StackItem>
                {
                    new StackItem("App.Initialize", 50),
                    new StackItem("Window.Show", 100, new List<StackItem>
                    {
                        new StackItem("PlatformImpl.Show", 90)
                    }),
                    new StackItem("Dispatcher.MainLoop", 1800, new List<StackItem>
                    {
                        new StackItem("InputManager.ProcessInput", 400, new List<StackItem>
                        {
                            new StackItem("MouseDevice.Process", 200),
                            new StackItem("KeyboardDevice.Process", 150)
                        }),
                        new StackItem("LayoutManager.ExecuteLayoutPass", 800, new List<StackItem>
                        {
                            new StackItem("MeasureOverride", 350, new List<StackItem>
                            {
                                new StackItem("Grid.Measure", 150),
                                new StackItem("StackPanel.Measure", 100),
                                new StackItem("TextBlock.Measure", 80)
                            }),
                            new StackItem("ArrangeOverride", 400, new List<StackItem>
                            {
                                new StackItem("Grid.Arrange", 180),
                                new StackItem("Border.Arrange", 100)
                            })
                        }),
                        new StackItem("RenderLoop.CustomDraw", 500, new List<StackItem>
                        {
                            new StackItem("Compositor.Render", 450, new List<StackItem>
                            {
                                new StackItem("SkiaRender.DrawContext", 400)
                            })
                        })
                    })
                })
            })
        };
    }

    [ObservableProperty] private ObservableCollection<TreeMapItem> _treeMapData = null!;
    [ObservableProperty] private ObservableCollection<SunburstNode> _sunburstData = null!;
    [ObservableProperty] private ObservableCollection<TreeMapItem> _icicleData = null!;
    [ObservableProperty] private ObservableCollection<TreeMapItem> _circlePackingData = null!;
    [ObservableProperty] private ObservableCollection<OrgNode> _orgChartData = null!;
    [ObservableProperty] private ObservableCollection<TreeNode> _dendrogramData = null!;
    [ObservableProperty] private ObservableCollection<TreeNode> _indentedTreeData = null!;
    [ObservableProperty] private ObservableCollection<TreeNode> _radialTreeData = null!;
    [ObservableProperty] private ObservableCollection<StackItem> _stackTraceData = null!;
}

// Data records for hierarchical charts
public record TreeMapItem(string Name, double Size);

public class SunburstNode
{
    public string Name { get; set; } = "";
    public double Size { get; set; }
    public List<SunburstNode>? Children { get; set; }
}

public class OrgNode
{
    public string Name { get; set; } = "";
    public List<OrgNode>? Reports { get; set; }
}

public class TreeNode
{
    public string Name { get; set; } = "";
    public List<TreeNode>? Children { get; set; }
}

public class StackItem
{
    public string MethodName { get; }
    public double Duration { get; }
    public List<StackItem> SubCalls { get; }

    public StackItem(string methodName, double duration, List<StackItem>? subCalls = null)
    {
        MethodName = methodName;
        Duration = duration;
        SubCalls = subCalls ?? new List<StackItem>();
    }
}
