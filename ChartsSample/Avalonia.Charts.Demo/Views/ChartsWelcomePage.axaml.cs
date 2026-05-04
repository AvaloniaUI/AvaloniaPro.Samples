using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.Controls.Charts;

namespace Avalonia.Charts.Demo.Views.Charts;

public partial class ChartsWelcomePage : UserControl
{
    private DispatcherTimer? _timer;
    private double _t = 0;
    private ObservableCollection<dynamic> _data;

    public ChartsWelcomePage()
    {
        InitializeComponent();
        _data = new ObservableCollection<dynamic>();
        
        // Initialize Hero Chart Data
        var heroSeries = this.FindControl<SplineAreaSeries>("HeroSeries");
        if (heroSeries != null)
        {
            for (int i = 0; i < 50; i++)
            {
                _data.Add(new { Item = i, Value = GetValue(i, _t) });
            }
            heroSeries.ItemsSource = _data;
        }

        // Manage timer based on visual tree attachment to handle navigation
        AttachedToVisualTree += (s, e) => StartTimer();
        DetachedFromVisualTree += (s, e) => StopTimer();
    }

    private void StartTimer()
    {
        if (_timer == null)
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(30)
            };
            _timer.Tick += OnTimerTick;
        }
        _timer.Start();
    }

    private void StopTimer()
    {
        _timer?.Stop();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        _t += 0.05;
        for (int i = 0; i < _data.Count; i++)
        {
            _data[i] = new { Item = i, Value = GetValue(i, _t) };
        }
    }

    private double GetValue(int index, double time)
    {
        return Math.Sin(index * 0.2 + time) * 10 + 
               Math.Sin(index * 0.5 + time * 2) * 5 + 
               20;
    }
}
