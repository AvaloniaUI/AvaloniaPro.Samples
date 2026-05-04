using System.Collections.Generic;

namespace Avalonia.Charts.Demo.Models;

/// <summary>
/// Simple hierarchical data item for TreeMap and other hierarchical charts.
/// </summary>
public class HierarchicalItem
{
    public string Name { get; set; }
    public double Value { get; set; }
    public List<HierarchicalItem>? Children { get; set; }

    public HierarchicalItem(string name, double value, List<HierarchicalItem>? children = null)
    {
        Name = name;
        Value = value;
        Children = children;
    }
}
