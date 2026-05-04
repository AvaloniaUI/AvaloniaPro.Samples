using System.Collections.Generic;

namespace Avalonia.Charts.Demo.Models;

/// <summary>
/// Simple node item for OrgChart samples.
/// </summary>
public class OrgNodeItem
{
    public string Name { get; set; } = "";
    public List<OrgNodeItem>? Reports { get; set; }
}
