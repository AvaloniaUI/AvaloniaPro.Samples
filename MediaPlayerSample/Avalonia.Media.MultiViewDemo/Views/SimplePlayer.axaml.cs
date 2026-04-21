using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media.MultiViewDemo.ViewModels;
using Avalonia.Platform.Storage;
using Avalonia.Rendering.Composition;

namespace Avalonia.Media.MultiViewDemo.Views
{
    public partial class SimplePlayer : UserControl
    {
        private readonly Layoutable[] _presenters;
        private int _currentPresenter;
        private Size _currentSize;

        private SimpleViewModel? Vm { get; set; }

        public SimplePlayer()
        {
            InitializeComponent();

            _presenters = new[] { _presenter1, _presenter2, _presenter3 };
            _currentPresenter = 0;

            DataContext = new SimpleViewModel();
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            Vm = DataContext as SimpleViewModel;

            if (Vm?.Player is { } player)
            {
                Vm.InitPlayer();
                player.UpdateTargetVisual(_presenters[_currentPresenter]);
                player.NaturalSizeChanged += Player_NaturalSizeChanged;
            }
        }

        private void Player_NaturalSizeChanged(object? sender, NaturalSizeChangedEventArgs e)
        {
            UpdatePlayerSize(e.NewSize ?? default);
        }

        protected override void OnUnloaded(RoutedEventArgs e)
        {
            base.OnUnloaded(e);

            Vm?.Player?.UnInitialize();
        }

        private void UpdatePlayerSize(Size size)
        {
            _currentSize = size;
            var presenter = _presenters[_currentPresenter];

            foreach (var p in _presenters)
            {
                var e = ElementComposition.GetElementChildVisual(p);
                var c = e?.Compositor;
            }

            var elemVisual = ElementComposition.GetElementChildVisual(presenter);
            var compositor = elemVisual?.Compositor;

            if (compositor is null || elemVisual is null)
                return;

            elemVisual.Size = new Vector(size.Width, size.Height);
            (presenter as MediaPlayerPresenter)?.SetNaturalSize(size);
            presenter.InvalidateMeasure();
            presenter.InvalidateArrange();
        }

        private void Slider_ValueChanged(object? sender, Avalonia.Controls.Primitives.RangeBaseValueChangedEventArgs e)
        {

        }

        private async void LoadButton_Click(object? sender, RoutedEventArgs e)
        {
            var storageProver = TopLevel.GetTopLevel(this)?.StorageProvider;
            if (storageProver is null || Vm is null)
                return;

            var files = await storageProver.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                AllowMultiple = false
            });

            if (files.Count != 1)
                return;
            if (files[0].Path is not { } path)
                return;

            Vm.Source = new StorageFileSource(files[0]);
        }

        private void ToggleButton_Click(object? sender, RoutedEventArgs e)
        {
            _currentPresenter = (++_currentPresenter) % _presenters.Length;

            if (Vm?.Player is { } player)
            {
                player.UpdateTargetVisual(_presenters[_currentPresenter]);
                UpdatePlayerSize(_currentSize);
            }
        }
    }
}
