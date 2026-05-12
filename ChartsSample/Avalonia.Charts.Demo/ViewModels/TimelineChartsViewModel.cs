using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class TimelineChartsViewModel : ViewModelBase
{
    public TimelineChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        var today = DateTime.Today;

        // Gantt Chart
        GanttTasks = new ObservableCollection<GanttTask>
        {
            new("Planning", today, today.AddDays(5), 100),
            new("Design", today.AddDays(3), today.AddDays(10), 80),
            new("Development", today.AddDays(8), today.AddDays(20), 48),
            new("Testing", today.AddDays(18), today.AddDays(25), 18),
            new("Deployment", today.AddDays(24), today.AddDays(28), 0)
        };

        // Event Timeline
        TimelineEvents = new ObservableCollection<TimelineEvent>
        {
            new(new DateTime(2024, 1, 15), "v1.0 Release"),
            new(new DateTime(2024, 4, 10), "v2.0 Beta"),
            new(new DateTime(2024, 7, 20), "v2.0 Release"),
            new(new DateTime(2024, 10, 5), "v3.0 Preview"),
            new(new DateTime(2024, 12, 1), "v3.0 Release")
        };

        // Spiral Timeline
        SpiralEvents = new ObservableCollection<SpiralEvent>
        {
            new(new DateTime(2024, 1, 1), "New Year", 1.0),
            new(new DateTime(2024, 3, 20), "Spring", 2.0),
            new(new DateTime(2024, 6, 21), "Summer", 3.0),
            new(new DateTime(2024, 9, 22), "Autumn", 2.0),
            new(new DateTime(2024, 12, 21), "Winter", 1.0)
        };

        // Swimlane Chart
        SwimlaneTasks = new ObservableCollection<SwimlaneTask>
        {
            new("Design", "Wireframes", 0.0, 3.0, Brushes.DodgerBlue),
            new("Design", "Mockups", 2.0, 5.0, Brushes.SteelBlue),
            new("Development", "Frontend", 4.0, 10.0, Brushes.SeaGreen),
            new("Development", "Backend", 3.0, 9.0, Brushes.MediumSeaGreen),
            new("Testing", "Unit Tests", 6.0, 10.0, Brushes.Orange),
            new("Testing", "Integration", 9.0, 12.0, Brushes.DarkOrange),
            new("Deployment", "Staging", 11.0, 13.0, Brushes.MediumPurple),
            new("Deployment", "Production", 13.0, 14.0, Brushes.Purple)
        };
    }

    [ObservableProperty] private ObservableCollection<GanttTask> _ganttTasks = null!;
    [ObservableProperty] private ObservableCollection<TimelineEvent> _timelineEvents = null!;
    [ObservableProperty] private ObservableCollection<SpiralEvent> _spiralEvents = null!;
    [ObservableProperty] private ObservableCollection<SwimlaneTask> _swimlaneTasks = null!;
}

public record GanttTask(string Name, DateTime Start, DateTime End, double Progress = 0);
public record TimelineEvent(DateTime Date, string Event);
public record SpiralEvent(DateTime Date, string Event, double Value);
public record SwimlaneTask(string Lane, string Task, double Start, double End, IBrush? Brush = null);
