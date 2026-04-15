using System;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Media.MultiViewDemo.ViewModels
{
    public partial class SimpleViewModel : ViewModelBase, IDisposable
    {
        [ObservableProperty] private MediaSource? _source;
        [ObservableProperty] private TimeSpan? _duration;
        [ObservableProperty] private TimeSpan? _position;
        [ObservableProperty] private long _ticks = 1;
        [ObservableProperty] private long _currentPosition;
        private bool _initialized;

        public MediaPlayer Player { get; } = new MediaPlayer();

        public SimpleViewModel()
        {
        }

        protected async override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(Source))
            {
                await Player.StopAsync();

                if (Source is not null)
                {
                    await Player.SetSourceAsync(Source);
                    await Player.PrepareAsync();
                }
            }
        }

        public async void InitPlayer()
        {
            if (_initialized)
                return;
            Player.LoadedBehavior = MediaPlayerLoadedBehavior.AutoPlay;
            await Player.InitializeAsync();

            Player.PropertyChanged += Player_PropertyChanged;


            _initialized = true;
        }

        private void Player_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Player.Duration))
            {
                Duration = Player.Duration;
                Ticks = Duration?.Ticks ?? 0;
            }
            else if(e.PropertyName == nameof(Player.Position))
            {
                var position = Player.Position;
                Ticks = long.Max(Ticks, position.Ticks);
                CurrentPosition = position.Ticks;
                Position = position;
            }
        }

        public async void PlayAsync()
        {
            await Player.PlayAsync();
        }

        public async void PauseAsync()
        {
            await Player.PauseAsync();
        }

        public async void StopAsync()
        {
            await Player.StopAsync();
        }

        public void Dispose()
        {
            _initialized = false;
        }
    }
}
