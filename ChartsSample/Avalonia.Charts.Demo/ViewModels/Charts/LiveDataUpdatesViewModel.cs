using System.Collections.ObjectModel;
using Avalonia.Charts.Demo.Models;
using Avalonia.Controls.Charts;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class LiveDataUpdatesViewModel : ViewModelBase
{
    private readonly Random _random = new(42);
    private readonly string[] _lineLabels = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct"];
    private readonly string[] _bubblePool = ["Search", "Payments", "Growth", "Ops", "Support", "AI", "Mobile", "Infra"];
    private bool _useAlternateLineSource;
    private bool _useAlternateBubbleSource;
    private bool _useAlternateTableSource;
    private bool _showMarginColumn = true;
    private bool _showDetailedRevenueHeader;
    private int _sankeyMutationCount;
    private int _wordCloudMutationCount;
    private int _vennMutationCount;
    private int _organizationMutationCount;

    [ObservableProperty]
    private ObservableCollection<ChartDataItem> _lineData = null!;

    [ObservableProperty]
    private ObservableCollection<LiveBubbleItem> _bubbleCloudData = null!;

    [ObservableProperty]
    private ObservableCollection<LiveTableRow> _tableRows = null!;

    [ObservableProperty]
    private ObservableCollection<TableChartColumn> _tableColumns = null!;

    [ObservableProperty]
    private ObservableCollection<LiveSankeyLink> _liveSankeyData = null!;

    [ObservableProperty]
    private ObservableCollection<LiveWordItem> _liveWordCloudData = null!;

    [ObservableProperty]
    private ObservableCollection<VennItem> _liveVennData = null!;

    [ObservableProperty]
    private ObservableCollection<OrgNode> _liveOrganizationData = null!;

    [ObservableProperty]
    private string _sankeyUpdateSummary = string.Empty;

    [ObservableProperty]
    private string _wordCloudUpdateSummary = string.Empty;

    [ObservableProperty]
    private string _vennUpdateSummary = string.Empty;

    [ObservableProperty]
    private string _organizationUpdateSummary = string.Empty;

    [ObservableProperty]
    private string _lineUpdateSummary = string.Empty;

    [ObservableProperty]
    private string _bubbleUpdateSummary = string.Empty;

    [ObservableProperty]
    private string _tableUpdateSummary = string.Empty;

    public LiveDataUpdatesViewModel()
    {
        ResetLineDataset();
        ResetBubbleDataset();
        ResetTableData();
        ResetTableColumns();
        ResetEnterpriseDatasets();
    }

    [RelayCommand]
    private void AddLinePoint()
    {
        var nextIndex = LineData.Count;
        var label = nextIndex < _lineLabels.Length ? _lineLabels[nextIndex] : $"P{nextIndex + 1}";
        var value = NextLineValue(LineData.LastOrDefault()?.Value ?? 36);
        LineData.Add(new ChartDataItem(label, value));
        LineUpdateSummary = $"Added {label} via ObservableCollection.Add.";
    }

    [RelayCommand]
    private void ReplaceLinePoint()
    {
        if (LineData.Count == 0)
        {
            return;
        }

        var index = _random.Next(LineData.Count);
        var current = LineData[index];
        var updated = NextLineValue(current.Value);
        LineData[index] = new ChartDataItem(current.Category, updated);
        LineUpdateSummary = $"Replaced {current.Category} in-place via ObservableCollection[index] = item.";
    }

    [RelayCommand]
    private void ResetLineData()
    {
        ResetLineDataset();
        LineUpdateSummary = "Reset the active line-series source without recreating the chart.";
    }

    [RelayCommand]
    private void SwapLineSource()
    {
        _useAlternateLineSource = !_useAlternateLineSource;
        ResetLineDataset();
        LineUpdateSummary = $"Swapped the LineSeries.ItemsSource reference to the {(_useAlternateLineSource ? "alternate" : "primary")} dataset.";
    }

    [RelayCommand]
    private void AddBubble()
    {
        var name = _bubblePool.FirstOrDefault(candidate => BubbleCloudData.All(item => item.Name != candidate))
                   ?? $"Node {BubbleCloudData.Count + 1}";
        BubbleCloudData.Add(new LiveBubbleItem(name, _random.Next(22, 95)));
        BubbleUpdateSummary = $"Added {name} to BubbleCloud.ItemsSource.";
    }

    [RelayCommand]
    private void ResizeBubble()
    {
        if (BubbleCloudData.Count == 0)
        {
            return;
        }

        var index = _random.Next(BubbleCloudData.Count);
        var current = BubbleCloudData[index];
        var nextValue = Math.Clamp(current.Value + _random.Next(-18, 22), 10, 100);
        BubbleCloudData[index] = current with { Value = nextValue };
        BubbleUpdateSummary = $"Replaced {current.Name} using ObservableCollection[index] = item.";
    }

    [RelayCommand]
    private void RemoveBubble()
    {
        if (BubbleCloudData.Count == 0)
        {
            return;
        }

        var smallest = BubbleCloudData.MinBy(item => item.Value)!;
        BubbleCloudData.Remove(smallest);
        BubbleUpdateSummary = $"Removed {smallest.Name} via ObservableCollection.Remove.";
    }

    [RelayCommand]
    private void SwapBubbleSource()
    {
        _useAlternateBubbleSource = !_useAlternateBubbleSource;
        ResetBubbleDataset();
        BubbleUpdateSummary = $"Swapped the BubbleCloud.ItemsSource reference to the {(_useAlternateBubbleSource ? "alternate" : "primary")} cluster.";
    }

    [RelayCommand]
    private void RenameRevenueColumn()
    {
        if (TableColumns.Count == 0)
        {
            return;
        }

        _showDetailedRevenueHeader = !_showDetailedRevenueHeader;
        TableColumns[0].Header = _showDetailedRevenueHeader ? "Revenue Run Rate" : "Revenue";
        TableUpdateSummary = "Updated a TableChartColumn property; the header redraws without replacing Columns.";
    }

    [RelayCommand]
    private void ToggleMarginColumn()
    {
        var existing = TableColumns.FirstOrDefault(column => column.ValuePath == nameof(LiveTableRow.Margin));
        if (existing is null)
        {
            TableColumns.Add(CreateMarginColumn());
            _showMarginColumn = true;
            TableUpdateSummary = "Added the Margin column via the live Columns collection.";
            return;
        }

        TableColumns.Remove(existing);
        _showMarginColumn = false;
        TableUpdateSummary = "Removed the Margin column via the live Columns collection.";
    }

    [RelayCommand]
    private void ReplaceTableRow()
    {
        if (TableRows.Count == 0)
        {
            return;
        }

        var index = _random.Next(TableRows.Count);
        var current = TableRows[index];
        var updated = current with
        {
            Revenue = Math.Clamp(current.Revenue + _random.Next(-180, 240), 800, 3000),
            Growth = Math.Clamp(current.Growth + _random.NextDouble() * 0.12 - 0.06, -0.10, 0.45),
            Margin = Math.Clamp(current.Margin + _random.NextDouble() * 0.10 - 0.05, 0.18, 0.62)
        };

        TableRows[index] = updated;
        TableUpdateSummary = $"Replaced the {current.Product} row through ObservableCollection[index] = item.";
    }

    [RelayCommand]
    private void SwapTableSource()
    {
        _useAlternateTableSource = !_useAlternateTableSource;
        ResetTableData();
        TableUpdateSummary = $"Swapped the TableChart.ItemsSource reference to the {(_useAlternateTableSource ? "alternate" : "primary")} dataset.";
    }

    [RelayCommand]
    private void MutateSankey()
    {
        _sankeyMutationCount++;
        var target = _sankeyMutationCount % 2 == 0 ? "Won" : "Review";
        var value = 4 + _sankeyMutationCount;
        LiveSankeyData.Add(new LiveSankeyLink("Qualified", target, value));
        SankeyUpdateSummary = $"Added Qualified -> {target} ({value}) to the Sankey collection only.";
    }

    [RelayCommand]
    private void MutateWordCloud()
    {
        if (LiveWordCloudData.Count == 0)
        {
            return;
        }

        _wordCloudMutationCount++;
        var index = _wordCloudMutationCount % LiveWordCloudData.Count;
        var current = LiveWordCloudData[index];
        var updated = current with { Weight = current.Weight + 8 };
        LiveWordCloudData[index] = updated;
        WordCloudUpdateSummary = $"Updated {current.Word} weight from {current.Weight} to {updated.Weight}.";
    }

    [RelayCommand]
    private void MutateVenn()
    {
        _vennMutationCount++;
        var targetSet = (_vennMutationCount % 3) switch
        {
            1 => "Charts",
            2 => "Desktop",
            _ => "Enterprise"
        };
        var replacementIndex = -1;

        for (var i = 0; i < LiveVennData.Count; i++)
        {
            var item = LiveVennData[i];
            if (item.Sets.Count == 1 && item.Sets[0] == targetSet)
            {
                replacementIndex = i;
                break;
            }
        }

        if (replacementIndex < 0)
        {
            return;
        }

        var current = LiveVennData[replacementIndex];
        var accent = (_vennMutationCount % 3) switch
        {
            1 => "#06B6D4",
            2 => "#F59E0B",
            _ => "#EC4899"
        };
        LiveVennData[replacementIndex] = new VennItem
        {
            Sets = [targetSet],
            Value = current.Value,
            Name = $"{targetSet} {_vennMutationCount}",
            Fill = new SolidColorBrush(Color.Parse(accent), 0.82)
        };
        VennUpdateSummary = $"Replaced the {targetSet} circle label and fill in the Venn collection only.";
    }

    [RelayCommand]
    private void MutateOrganization()
    {
        _organizationMutationCount++;
        var root = LiveOrganizationData[0];
        root.Reports ??= [];
        root.Reports.Add(new OrgNode { Name = $"Team {_organizationMutationCount}" });
        LiveOrganizationData[0] = root;
        OrganizationUpdateSummary = $"Added Team {_organizationMutationCount} under Product.";
    }

    private void ResetLineDataset()
    {
        LineData = CreateLineDataset(_useAlternateLineSource);
        if (string.IsNullOrEmpty(LineUpdateSummary))
        {
            LineUpdateSummary = "Use Add, Replace, Reset, and Swap Source to exercise live series updates.";
        }
    }

    private void ResetBubbleDataset()
    {
        BubbleCloudData = CreateBubbleDataset(_useAlternateBubbleSource);
        if (string.IsNullOrEmpty(BubbleUpdateSummary))
        {
            BubbleUpdateSummary = "BubbleCloud reacts to collection mutations and ItemsSource replacement.";
        }
    }

    private void ResetTableData()
    {
        TableRows = CreateTableRows(_useAlternateTableSource);
        if (string.IsNullOrEmpty(TableUpdateSummary))
        {
            TableUpdateSummary = "TableChart now keeps both data rows and column definitions live.";
        }
    }

    private void ResetTableColumns()
    {
        TableColumns = new ObservableCollection<TableChartColumn>
        {
            new()
            {
                Header = "Revenue",
                ValuePath = nameof(LiveTableRow.Revenue),
                Format = "C0",
                UseColorScale = true,
                MinValue = 900,
                MaxValue = 2600,
                LowBrush = new SolidColorBrush(Color.FromRgb(227, 242, 253)),
                HighBrush = new SolidColorBrush(Color.FromRgb(21, 101, 192))
            },
            new()
            {
                Header = "Growth",
                ValuePath = nameof(LiveTableRow.Growth),
                Format = "P0",
                UseColorScale = true,
                MinValue = -0.10,
                MaxValue = 0.45,
                LowBrush = new SolidColorBrush(Color.FromRgb(255, 235, 238)),
                HighBrush = new SolidColorBrush(Color.FromRgb(46, 125, 50))
            }
        };

        if (_showMarginColumn)
        {
            TableColumns.Add(CreateMarginColumn());
        }
    }

    private ObservableCollection<ChartDataItem> CreateLineDataset(bool alternate)
    {
        var seedValues = alternate
            ? new[] { 48.0, 44.0, 58.0, 63.0, 56.0, 69.0 }
            : new[] { 32.0, 38.0, 35.0, 47.0, 52.0, 49.0 };

        var points = new ObservableCollection<ChartDataItem>();
        for (var i = 0; i < seedValues.Length; i++)
        {
            points.Add(new ChartDataItem(_lineLabels[i], seedValues[i]));
        }

        return points;
    }

    private ObservableCollection<LiveBubbleItem> CreateBubbleDataset(bool alternate)
    {
        return alternate
            ? new ObservableCollection<LiveBubbleItem>
            {
                new("Latency", 72),
                new("Throughput", 58),
                new("Backlog", 41),
                new("Errors", 27),
                new("Cache", 35)
            }
            : new ObservableCollection<LiveBubbleItem>
            {
                new("Search", 64),
                new("Payments", 48),
                new("Growth", 36),
                new("Support", 28),
                new("Mobile", 22)
            };
    }

    private ObservableCollection<LiveTableRow> CreateTableRows(bool alternate)
    {
        return alternate
            ? new ObservableCollection<LiveTableRow>
            {
                new("North", 2300, 0.18, 0.42),
                new("South", 1850, 0.11, 0.34),
                new("East", 1625, 0.07, 0.29),
                new("West", 2080, 0.22, 0.38)
            }
            : new ObservableCollection<LiveTableRow>
            {
                new("Core", 1750, 0.12, 0.36),
                new("Pro", 2140, 0.27, 0.44),
                new("Teams", 1410, 0.09, 0.31),
                new("Enterprise", 2525, 0.19, 0.48)
            };
    }

    private TableChartColumn CreateMarginColumn() =>
        new()
        {
            Header = "Margin",
            ValuePath = nameof(LiveTableRow.Margin),
            Format = "P0",
            UseColorScale = true,
            MinValue = 0.18,
            MaxValue = 0.62,
            LowBrush = new SolidColorBrush(Color.FromRgb(255, 243, 224)),
            HighBrush = new SolidColorBrush(Color.FromRgb(239, 108, 0))
        };

    private double NextLineValue(double previousValue) =>
        Math.Clamp(previousValue + _random.Next(-9, 12), 18, 82);

    private void ResetEnterpriseDatasets()
    {
        LiveSankeyData = new ObservableCollection<LiveSankeyLink>
        {
            new("Visits", "Qualified", 42),
            new("Visits", "Dropped", 18),
            new("Qualified", "Trial", 24),
            new("Qualified", "Sales", 18),
            new("Trial", "Won", 12),
            new("Sales", "Won", 14)
        };

        LiveWordCloudData = new ObservableCollection<LiveWordItem>
        {
            new("Charts", 58),
            new("Live", 42),
            new("Enterprise", 36),
            new("Selection", 28),
            new("Legend", 24),
            new("Axes", 20)
        };

        LiveVennData = new ObservableCollection<VennItem>
        {
            new() { Sets = ["Charts"], Value = 16, Name = "Charts", Fill = new SolidColorBrush(Color.Parse("#EF4444"), 0.82) },
            new() { Sets = ["Desktop"], Value = 13, Name = "Desktop", Fill = new SolidColorBrush(Color.Parse("#8B5CF6"), 0.82) },
            new() { Sets = ["Enterprise"], Value = 11, Name = "Enterprise", Fill = new SolidColorBrush(Color.Parse("#F97316"), 0.82) },
            new() { Sets = ["Charts", "Desktop"], Value = 6, Name = "Shared UI", Fill = new SolidColorBrush(Colors.Gray, 0.55) },
            new() { Sets = ["Charts", "Enterprise"], Value = 5, Name = "Advanced", Fill = new SolidColorBrush(Colors.Gray, 0.55) }
        };

        LiveOrganizationData = new ObservableCollection<OrgNode>
        {
            new()
            {
                Name = "Product",
                Reports =
                [
                    new() { Name = "Charts" },
                    new() { Name = "Enterprise" },
                    new() { Name = "Samples" }
                ]
            }
        };

        SankeyUpdateSummary = "Use this section's button to mutate only the Sankey data.";
        WordCloudUpdateSummary = "Use this section's button to mutate only the WordCloud data.";
        VennUpdateSummary = "Use this section's button to replace one visible Venn circle.";
        OrganizationUpdateSummary = "Use this section's button to mutate only the organization data.";
    }
}

public sealed record LiveBubbleItem(string Name, double Value);

public sealed record LiveTableRow(string Product, double Revenue, double Growth, double Margin);

public sealed record LiveSankeyLink(string Source, string Target, double Value);

public sealed record LiveWordItem(string Word, double Weight);
