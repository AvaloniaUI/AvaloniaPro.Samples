using Avalonia.Charts.Demo.Models;

namespace Avalonia.Charts.Demo.ViewModels;

public sealed class DiagnosticsViewModel : ViewModelBase
{
    public DiagnosticsViewModel()
    {
        CpuData = CreateWaveData(240, 45, 22, 0.16, 0.04);
        MemoryData = CreateWaveData(240, 62, 11, 0.10, 0.11);
        ThroughputData =
        [
            new("API", 48),
            new("Render", 64),
            new("Input", 32),
            new("Layout", 41),
            new("Export", 23),
            new("Tooling", 36)
        ];
        SegmentData =
        [
            new("Cartesian", 46),
            new("Circular", 24),
            new("Grid", 14),
            new("Other", 16)
        ];
    }

    public IReadOnlyList<double> CpuData { get; }
    public IReadOnlyList<double> MemoryData { get; }
    public IReadOnlyList<ChartDataItem> ThroughputData { get; }
    public IReadOnlyList<ChartDataItem> SegmentData { get; }

    private static double[] CreateWaveData(int count, double baseline, double amplitude, double frequency, double drift)
    {
        var data = new double[count];
        var random = new Random(7);

        for (var i = 0; i < data.Length; i++)
        {
            var wave = Math.Sin(i * frequency) * amplitude;
            var secondary = Math.Cos(i * frequency * 0.37) * amplitude * 0.28;
            var noise = (random.NextDouble() - 0.5) * amplitude * 0.18;
            data[i] = Math.Clamp(baseline + wave + secondary + i * drift + noise, 0, 100);
        }

        return data;
    }
}
