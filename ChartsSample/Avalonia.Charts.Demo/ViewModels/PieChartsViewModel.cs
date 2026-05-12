using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class PieChartsViewModel : ViewModelBase
{
    public PieChartsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Pie Chart
        PieChartData = new ObservableCollection<double> { 35, 25, 20, 15, 5 };

        // Donut Chart
        DonutChartData = new ObservableCollection<double> { 40, 30, 20, 10 };

        // Semi-Donut Chart
        SemiDonutChartData = new ObservableCollection<SemiDonutPoint>
        {
            new SemiDonutPoint("Product A", 450),
            new SemiDonutPoint("Product B", 320),
            new SemiDonutPoint("Product C", 210)
        };
    }

    [ObservableProperty] private ObservableCollection<double> _pieChartData = null!;
    [ObservableProperty] private ObservableCollection<double> _donutChartData = null!;
    [ObservableProperty] private ObservableCollection<SemiDonutPoint> _semiDonutChartData = null!;
}

public record SemiDonutPoint(string Label, double Value);
