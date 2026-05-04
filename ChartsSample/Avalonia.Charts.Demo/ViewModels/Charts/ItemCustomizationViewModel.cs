using System.Collections.ObjectModel;
using Avalonia.Media;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public partial class ItemCustomizationViewModel : ViewModelBase
{
    public ObservableCollection<CustomItem> StandardItems { get; } = new();
    public ObservableCollection<CustomItem> ConditionalItems { get; } = new();
    public ObservableCollection<CustomItem> ScatterItems { get; } = new();
    public ObservableCollection<CustomItem> LineItems { get; } = new();
    public ObservableCollection<CustomItem> GradientItems { get; } = new();
    public ObservableCollection<CustomItem> PieItems { get; } = new();

    public ItemCustomizationViewModel()
    {
        InitializeStandardData();
        InitializeConditionalData();
        InitializeScatterData();
        InitializeLineData();
        InitializeGradientData();
        InitializePieData();
    }

    private void InitializeStandardData()
    {
        StandardItems.Add(new CustomItem("Apples", 45, Brushes.IndianRed));
        StandardItems.Add(new CustomItem("Bananas", 80, Brushes.Gold));
        StandardItems.Add(new CustomItem("Grapes", 65, Brushes.MediumPurple));
        StandardItems.Add(new CustomItem("Oranges", 55, Brushes.Orange));
        StandardItems.Add(new CustomItem("Cherries", 30, Brushes.Crimson));
    }

    private void InitializeConditionalData()
    {
        // Profit/Loss scenario: Green for profit, Red for loss
        ConditionalItems.Add(new CustomItem("Jan", 120, Brushes.SeaGreen));
        ConditionalItems.Add(new CustomItem("Feb", -45, Brushes.Crimson));
        ConditionalItems.Add(new CustomItem("Mar", 85, Brushes.SeaGreen));
        ConditionalItems.Add(new CustomItem("Apr", -20, Brushes.Crimson));
        ConditionalItems.Add(new CustomItem("May", 150, Brushes.SeaGreen));
        ConditionalItems.Add(new CustomItem("Jun", 30, Brushes.SeaGreen));
    }

    private void InitializeScatterData()
    {
        var rnd = new Random(42);
        for (int i = 0; i < 20; i++)
        {
            var val = rnd.Next(10, 100);
            var category = i;
            // Outliers (val > 80) are Red, others Blue
            var color = val > 80 ? Brushes.Red : Brushes.DodgerBlue;
            ScatterItems.Add(new CustomItem(category.ToString(), val, color));
        }
    }

    private void InitializeLineData()
    {
        LineItems.Add(new CustomItem("1", 40, Brushes.DodgerBlue));
        LineItems.Add(new CustomItem("2", 30, Brushes.DodgerBlue));
        LineItems.Add(new CustomItem("3", 55, Brushes.Orange)); // Segment from 2 to 3 will use Orange
        LineItems.Add(new CustomItem("4", 45, Brushes.Orange));
        LineItems.Add(new CustomItem("5", 70, Brushes.Crimson)); // Segment from 4 to 5 will use Crimson
        LineItems.Add(new CustomItem("6", 60, Brushes.Crimson));
    }

    private void InitializeGradientData()
    {
        var blueGradient = new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
            EndPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            GradientStops =
            {
                new GradientStop(Color.Parse("#1E88E5"), 0),
                new GradientStop(Color.Parse("#90CAF9"), 1)
            }
        };

        var orangeGradient = new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
            EndPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            GradientStops =
            {
                new GradientStop(Color.Parse("#F4511E"), 0),
                new GradientStop(Color.Parse("#FFB74D"), 1)
            }
        };

        var greenGradient = new LinearGradientBrush
        {
            StartPoint = new RelativePoint(0, 1, RelativeUnit.Relative),
            EndPoint = new RelativePoint(0, 0, RelativeUnit.Relative),
            GradientStops =
            {
                new GradientStop(Color.Parse("#43A047"), 0),
                new GradientStop(Color.Parse("#A5D6A7"), 1)
            }
        };

        GradientItems.Add(new CustomItem("Blue", 75, blueGradient));
        GradientItems.Add(new CustomItem("Orange", 50, orangeGradient));
        GradientItems.Add(new CustomItem("Green", 90, greenGradient));
    }

    private void InitializePieData()
    {
        PieItems.Add(new CustomItem("Chrome", 65, new SolidColorBrush(Color.Parse("#4285F4"))));
        PieItems.Add(new CustomItem("Safari", 18, new SolidColorBrush(Color.Parse("#0096D6"))));
        PieItems.Add(new CustomItem("Firefox", 3, new SolidColorBrush(Color.Parse("#FF7139"))));
        PieItems.Add(new CustomItem("Edge", 4, new SolidColorBrush(Color.Parse("#0078D7"))));
        PieItems.Add(new CustomItem("Others", 10, new SolidColorBrush(Color.Parse("#9E9E9E"))));
    }
}

/// <summary>
/// A simple data item with a Name, Value, and Color for customization demos.
/// </summary>
public class CustomItem
{
    public string Name { get; set; } = string.Empty;
    public double Value { get; set; }
    public IBrush Color { get; set; } = Brushes.Transparent;

    public CustomItem() { }

    public CustomItem(string name, double value, IBrush color)
    {
        Name = name;
        Value = value;
        Color = color;
    }
}
