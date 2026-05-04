using System.Collections.ObjectModel;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class AnnotationsViewModel : ViewModelBase
{
    public ObservableCollection<double> ReferenceData { get; }
    public ObservableCollection<double> BandsData { get; }
    public ObservableCollection<Point> TextData { get; }
    public ObservableCollection<Point> ShapeData { get; }
    public ObservableCollection<Point> QuadrantData { get; }

    public AnnotationsViewModel()
    {
        ReferenceData = new ObservableCollection<double> { 45, 52, 38, 65, 70, 55, 80 };
        BandsData = new ObservableCollection<double> { 22, 25, 28, 35, 32, 28, 24 };
        
        TextData = new ObservableCollection<Point>
        {
            new Point(2, 20),
            new Point(5, 80),
            new Point(8, 40)
        };

        ShapeData = new ObservableCollection<Point>
        {
            new Point(3, 30),
            new Point(7, 70)
        };

        QuadrantData = new ObservableCollection<Point>
        {
            new Point(10, 80),
            new Point(30, 60),
            new Point(60, 90),
            new Point(80, 70),
            new Point(20, 30),
            new Point(40, 10),
            new Point(70, 40),
            new Point(90, 20)
        };
    }
}
