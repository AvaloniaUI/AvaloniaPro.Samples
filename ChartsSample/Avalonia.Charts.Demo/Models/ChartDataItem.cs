namespace Avalonia.Charts.Demo.Models;

/// <summary>
/// Simple data item for chart samples.
/// </summary>
public class ChartDataItem
{
    public string Category { get; set; } = string.Empty;
    public double Value { get; set; }

    public ChartDataItem()
    {
    }

    public ChartDataItem(string category, double value)
    {
        Category = category;
        Value = value;
    }
}
