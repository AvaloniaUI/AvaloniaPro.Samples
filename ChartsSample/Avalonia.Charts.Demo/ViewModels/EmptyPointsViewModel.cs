using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class EmptyPointsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<DataItem> _data;

    [ObservableProperty]
    private EmptyPointMode _selectedMode = EmptyPointMode.Gap;

    [ObservableProperty]
    private string _currentModeDescription = string.Empty;

    public ObservableCollection<EmptyPointMode> Modes { get; } = new ObservableCollection<EmptyPointMode>
    {
        EmptyPointMode.Gap,
        EmptyPointMode.Zero,
        EmptyPointMode.Average,
        EmptyPointMode.Interpolate
    };

    public EmptyPointsViewModel()
    {
        _data = new ObservableCollection<DataItem>
        {
            new DataItem("A", 10),
            new DataItem("B", 15),
            new DataItem("C", double.NaN), // Gap
            new DataItem("D", 20),
            new DataItem("E", double.NaN), // Gap
            new DataItem("F", 25),
            new DataItem("G", 18),
            new DataItem("H", 12)
        };

        UpdateDescription();
    }

    partial void OnSelectedModeChanged(EmptyPointMode value)
    {
        UpdateDescription();
    }

    private void UpdateDescription()
    {
        CurrentModeDescription = SelectedMode switch
        {
            EmptyPointMode.Gap => "Gap: Breaks the line at null/NaN values.",
            EmptyPointMode.Zero => "Zero: Treats null/NaN values as 0.",
            EmptyPointMode.Average => "Average: Calculates the average of neighbor points.",
            EmptyPointMode.Interpolate => "Interpolate: Connects the previous and next valid points.",
            _ => ""
        };
    }
}

public class DataItem
{
    public string Category { get; set; }
    public double Value { get; set; }

    public DataItem(string category, double value)
    {
        Category = category;
        Value = value;
    }
}
