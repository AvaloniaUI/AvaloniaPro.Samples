using System.Diagnostics;

namespace Avalonia.Charts.Demo.ViewModels.Charts;

public class BenchmarkChartsViewModel : ViewModelBase
{
    private string _status10K = "Waiting...";
    private string _status100K = "Waiting...";
    private string _status1M = "Waiting...";
    private IReadOnlyList<double> _data10K;
    private IReadOnlyList<double> _data100K;
    private IReadOnlyList<double> _data1M;

    public BenchmarkChartsViewModel()
    {
        _data10K = Array.Empty<double>();
        _data100K = Array.Empty<double>();
        _data1M = Array.Empty<double>();
    }

    public string Status10K
    {
        get => _status10K;
        set => SetProperty(ref _status10K, value);
    }

    public string Status100K
    {
        get => _status100K;
        set => SetProperty(ref _status100K, value);
    }

    public string Status1M
    {
        get => _status1M;
        set => SetProperty(ref _status1M, value);
    }

    public IReadOnlyList<double> Data10K
    {
        get => _data10K;
        set => SetProperty(ref _data10K, value);
    }

    public IReadOnlyList<double> Data100K
    {
        get => _data100K;
        set => SetProperty(ref _data100K, value);
    }

    public IReadOnlyList<double> Data1M
    {
        get => _data1M;
        set => SetProperty(ref _data1M, value);
    }

    public async Task<(double[] Data, double DurationMs)> GenerateDataAsync(int count)
    {
        var sw = Stopwatch.StartNew();
        var data = await Task.Run(() => GenerateRandomWalk(count));
        sw.Stop();
        return (data, sw.Elapsed.TotalMilliseconds);
    }

    private static double[] GenerateRandomWalk(int count)
    {
        var data = new double[count];
        var random = new Random(42);
        double value = 100;
        
        for (int i = 0; i < count; i++)
        {
            value += (random.NextDouble() - 0.5) * 2;
            data[i] = value;
        }
        
        return data;
    }
}
