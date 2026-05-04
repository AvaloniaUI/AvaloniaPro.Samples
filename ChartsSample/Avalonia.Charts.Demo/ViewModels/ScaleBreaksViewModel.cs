using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class ScaleBreaksViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<DataItem> _data;

    [ObservableProperty]
    private ObservableCollection<ScaleBreak> _breaks;
    
    [ObservableProperty]
    private bool _isBreakEnabled = true;

    [ObservableProperty]
    private ObservableCollection<DataItem> _multiBreakData;

    [ObservableProperty]
    private ObservableCollection<DataItem> _comboData;

    [ObservableProperty]
    private ObservableCollection<ScaleBreak> _comboBreaks;

    [ObservableProperty]
    private ObservableCollection<DataItem> _comboLineData;

    [ObservableProperty]
    private ObservableCollection<ContinuousNumericPoint> _continuousXBreakData;

    public ScaleBreaksViewModel()
    {
        Data = new ObservableCollection<DataItem>
        {
            new DataItem("A", 10),
            new DataItem("B", 20),
            new DataItem("C", 1000), // Gap starts here
            new DataItem("D", 1010),
            new DataItem("E", 100000), // Huge jump
            new DataItem("F", 100050),
            new DataItem("G", 100100)
        };
        
        // Data for multiple breaks: mostly around 0-200, 500, 900
        MultiBreakData = new ObservableCollection<DataItem>
        {
            new DataItem("Low 1", 50),
            new DataItem("Low 2", 150),
            new DataItem("Low 3", 200),
            new DataItem("Mid 1", 500), // In between breaks
            new DataItem("Mid 2", 550),
            new DataItem("High 1", 900),
            new DataItem("High 2", 950),
        };

        Breaks = new ObservableCollection<ScaleBreak>
        {
            new ScaleBreak
            {
                Start = 1100,
                End = 99900,
                Stroke = global::Avalonia.Media.Brushes.Red,
                StrokeThickness = 1
            }
        };

        ComboData = new ObservableCollection<DataItem>
        {
            new DataItem("Q1", 40),
            new DataItem("Q2", 80), 
            new DataItem("Q3", 600),
            new DataItem("Q4", 650)
        };

        ComboLineData = new ObservableCollection<DataItem>
        {
            new DataItem("Q1", 30),
            new DataItem("Q2", 90),
            new DataItem("Q3", 580),
            new DataItem("Q4", 620)
        };

        ComboBreaks = new ObservableCollection<ScaleBreak>
        {
            new ScaleBreak
            {
                Start = 150,
                End = 500,
                Stroke = global::Avalonia.Media.Brushes.DarkSlateBlue,
                StrokeThickness = 1
            }
        };

        ContinuousXBreakData = new ObservableCollection<ContinuousNumericPoint>
        {
            new(0, 32),
            new(10, 36),
            new(22, 34),
            new(42, 40),
            new(112, 66),
            new(132, 70),
            new(158, 74)
        };
    }

    partial void OnIsBreakEnabledChanged(bool value)
    {
        if (value)
        {
            if (Breaks.Count == 0)
            {
                 Breaks.Add(new ScaleBreak
                 {
                     Start = 1100,
                     End = 99900,
                     Stroke = global::Avalonia.Media.Brushes.Red,
                     StrokeThickness = 1
                 });
            }
        }
        else
        {
            Breaks.Clear();
        }
    }
}
