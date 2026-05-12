using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class ExportingViewModel : ViewModelBase
{
    public ObservableCollection<double> RevenueData { get; }

    public ExportingViewModel()
    {
        RevenueData = new ObservableCollection<double> { 120, 150, 180, 220, 280, 350 };
    }
}
