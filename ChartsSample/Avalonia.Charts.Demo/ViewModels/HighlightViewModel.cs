using System.Collections.ObjectModel;
using Avalonia.Charts.Demo.Models;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class HighlightViewModel
{
    public ObservableCollection<ChartDataItem> DonutData { get; } = new()
    {
        new("Housing", 1200),
        new("Food", 450),
        new("Transport", 350),
        new("Entertainment", 200),
        new("Utilities", 180),
        new("Other", 120)
    };

    public ObservableCollection<ChartDataItem> PieData { get; } = new()
    {
        new("Chrome", 65),
        new("Safari", 18),
        new("Firefox", 7),
        new("Edge", 5),
        new("Other", 5)
    };

    public ObservableCollection<int> BarData { get; } = new() { 35, 28, 42, 31, 45, 38, 50 };

    public ObservableCollection<int> LineData1 { get; } = new() { 20, 25, 30, 28, 35, 32, 38 };
    public ObservableCollection<int> LineData2 { get; } = new() { 30, 32, 28, 35, 40, 38, 45 };
    public ObservableCollection<int> LineData3 { get; } = new() { 15, 20, 25, 30, 28, 35, 40 };

    public ObservableCollection<string> RadarLabels { get; } = new()
    {
        "Speed", "Quality", "Cost", "Risk", "Reach"
    };

    public ObservableCollection<double> RadarData { get; } = new()
    {
        82, 74, 68, 55, 91
    };

    public List<SelectionItem> SunburstData { get; } =
    [
        new()
        {
            Name = "Platform",
            Value = 100,
            Children =
            [
                new() { Name = "Charts", Value = 45 },
                new() { Name = "Inputs", Value = 25 },
                new() { Name = "Layout", Value = 30 }
            ]
        },
        new()
        {
            Name = "Services",
            Value = 70,
            Children =
            [
                new() { Name = "Auth", Value = 25 },
                new() { Name = "Billing", Value = 20 },
                new() { Name = "Search", Value = 25 }
            ]
        }
    ];

    public List<SelectionItem> TreeMapData { get; } =
    [
        new() { Name = "Rendering", Value = 42 },
        new() { Name = "Interaction", Value = 27 },
        new() { Name = "Data", Value = 35 },
        new() { Name = "Export", Value = 18 }
    ];

    public ObservableCollection<VennItem> VennData { get; } = new()
    {
        new() { Sets = ["Desktop"], Value = 18, Name = "Desktop" },
        new() { Sets = ["Mobile"], Value = 12, Name = "Mobile" },
        new() { Sets = ["Desktop", "Mobile"], Value = 7, Name = "Shared" }
    };
}
