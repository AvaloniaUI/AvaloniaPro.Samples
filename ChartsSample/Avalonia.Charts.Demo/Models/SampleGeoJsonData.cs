using System;
using System.IO;
using System.Reflection;

namespace Avalonia.Charts.Demo.Models;

public static class SampleGeoJsonData
{
    private static string? _cachedWorldGeoJson;
    private static string? _cachedSpainGeoJson;

    public static string World => GetEmbeddedWorldGeoJson();
    public static string Spain => GetEmbeddedSpainGeoJson();

    private static string GetEmbeddedWorldGeoJson()
    {
        if (_cachedWorldGeoJson != null) return _cachedWorldGeoJson;

        try
        {
            var assembly = typeof(SampleGeoJsonData).Assembly;
            var resourceName = "Avalonia.Charts.Demo.Resources.ne_110m_world.geojson";
            
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                _cachedWorldGeoJson = reader.ReadToEnd();
                return _cachedWorldGeoJson;
            }
        }
        catch
        {
            // Fall through
        }

        return """{"type":"FeatureCollection","features":[]}""";
    }

    private static string GetEmbeddedSpainGeoJson()
    {
        if (_cachedSpainGeoJson != null) return _cachedSpainGeoJson;

        try
        {
            var assembly = typeof(SampleGeoJsonData).Assembly;
            var resourceName = "Avalonia.Charts.Demo.Resources.spain_communities.geojson";
            
            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                _cachedSpainGeoJson = reader.ReadToEnd();
                return _cachedSpainGeoJson;
            }
        }
        catch
        {
            // Fall through
        }

        return """{"type":"FeatureCollection","features":[]}""";
    }
}
