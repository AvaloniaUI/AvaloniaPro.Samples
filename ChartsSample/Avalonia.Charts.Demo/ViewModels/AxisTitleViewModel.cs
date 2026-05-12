using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class AxisTitleViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<double> _basicData = null!;

    [ObservableProperty]
    private ObservableCollection<double> _styledData = null!;

    public AxisTitleViewModel()
    {
        InitializeData();
    }

    private void InitializeData()
    {
        BasicData = new ObservableCollection<double> { 120, 150, 180, 200, 250 };
        StyledData = new ObservableCollection<double> { 10, 25, 15, 40, 60 };
    }
}
