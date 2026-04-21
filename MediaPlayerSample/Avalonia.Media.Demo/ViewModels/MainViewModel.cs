using System.Diagnostics;
using System.Windows.Input;
using Avalonia.Media.Demo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Media.Demo.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty] private RemovableMediaView? _currentMediaView;

        [ObservableProperty] private MediaSource? _source;
        [ObservableProperty] private bool _isBuffering;
        [ObservableProperty] private double? _bufferingProgress;


        [ObservableProperty] private ICommand? _dropCommand;

        [ObservableProperty] private ICommand? _fullScreenCommand;

        [ObservableProperty] private ICommand? _dragMoveCommand;

        [ObservableProperty] private bool _isFullScreen;

        [ObservableProperty] private bool _enableTransparency;

        [ObservableProperty] private bool _showStats;

        [ObservableProperty] private Statistics _stats = new Statistics(null);

        [ObservableProperty] private RelayCommand<bool> _toggleMediaViewCommand;

        private void OnToggleMediaViewCommand(bool activate)
        {
            CurrentMediaView = activate ? new RemovableMediaView() : null;
        }

        public void RaiseStatsChanged()
        {
            OnPropertyChanged(nameof(Stats.DecodedVideo));
        }

        public MainViewModel()
        {
            ToggleMediaViewCommand = new(OnToggleMediaViewCommand);

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(BufferingProgress))
                    Debug.WriteLine(BufferingProgress);
                if (args.PropertyName == nameof(Source) && CurrentMediaView is not null)
                    CurrentMediaView.Source = Source;
            };

            CurrentMediaView = new RemovableMediaView();
        }

        public void SetSource(UriSource uriSource)
        {
            // Just call prop changed for now as a HACK.
            Source = uriSource;
            OnPropertyChanged(nameof(Source));
        }
    }
}
