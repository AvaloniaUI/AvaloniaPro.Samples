using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class AxisCustomizationViewModel : ViewModelBase
{
    public AxisCustomizationViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Sample data for all customization demos
        SalesData = new ObservableCollection<int> { 35, 28, 34, 32, 40, 32, 35 };
        MonthlyData = new ObservableCollection<int> { 10, 25, 30, 45, 60, 55, 70, 65, 80, 75, 90, 85 };
        QuarterlyData = new ObservableCollection<int> { 150, 200, 180, 220 };
        Categories = new ObservableCollection<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul" };
        LongCategories = new ObservableCollection<string> 
        { 
            "January 2024", "February 2024", "March 2024", "April 2024", 
            "May 2024", "June 2024", "July 2024"
        };
    }

    [ObservableProperty] private ObservableCollection<int> _salesData = null!;
    [ObservableProperty] private ObservableCollection<int> _monthlyData = null!;
    [ObservableProperty] private ObservableCollection<int> _quarterlyData = null!;
    [ObservableProperty] private ObservableCollection<string> _categories = null!;
    [ObservableProperty] private ObservableCollection<string> _longCategories = null!;
}
