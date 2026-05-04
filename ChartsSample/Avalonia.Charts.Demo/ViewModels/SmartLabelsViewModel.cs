using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class SmartLabelsViewModel : ViewModelBase
{
    public SmartLabelsViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        // Monthly Sales (Long Categories)
        var longCategories = new[]
        {
            "January 2024", "February 2024", "March 2024", "April 2024", 
            "May 2024", "June 2024", "July 2024", "August 2024",
            "September 2024", "October 2024", "November 2024", "December 2024"
        };
        var values = new[] { 45, 52, 48, 60, 55, 70, 65, 75, 68, 80, 72, 85 };

        MonthlySales = new ObservableCollection<SmartLabelItem>();
        for (int i = 0; i < longCategories.Length; i++)
        {
            MonthlySales.Add(new SmartLabelItem(longCategories[i], values[i]));
        }

        // Category Revenue (Wrap Categories)
        var wrapCategories = new[]
        {
            "Product Category A", "Product Category B", "Product Category C",
            "Product Category D", "Product Category E", "Product Category F"
        };
        var revenueValues = new[] { 120, 150, 180, 140, 160, 190 };

        CategoryRevenue = new ObservableCollection<SmartLabelItem>();
        for (int i = 0; i < wrapCategories.Length; i++)
        {
            CategoryRevenue.Add(new SmartLabelItem(wrapCategories[i], revenueValues[i]));
        }

        // Quarterly Growth (Manual Rotation)
        var growthCategories = new[]
        {
            "Q1 2024", "Q2 2024", "Q3 2024", "Q4 2024",
            "Q1 2025", "Q2 2025", "Q3 2025", "Q4 2025"
        };
        var growthValues = new[] { 10, 15, 22, 28, 35, 42, 50, 58 };

        QuarterlyGrowth = new ObservableCollection<SmartLabelItem>();
        for (int i = 0; i < growthCategories.Length; i++)
        {
            QuarterlyGrowth.Add(new SmartLabelItem(growthCategories[i], growthValues[i]));
        }
    }

    [ObservableProperty] private ObservableCollection<SmartLabelItem> _monthlySales = null!;
    [ObservableProperty] private ObservableCollection<SmartLabelItem> _categoryRevenue = null!;
    [ObservableProperty] private ObservableCollection<SmartLabelItem> _quarterlyGrowth = null!;
}

public record SmartLabelItem(string Category, double Value);
