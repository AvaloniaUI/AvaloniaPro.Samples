using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Labs.Controls;
using Avalonia.Media.Demo.ViewModels;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.Input;

namespace Avalonia.Media.Demo.Views
{
    public partial class MainView : UserControl
    {
        private MediaPlayerControl? _subscribedPlayer;
        private DispatcherTimer? _toastTimer;

        public MainView()
        {
            InitializeComponent();
        }

        private MainViewModel? MainVm { get; set; }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            MainVm = DataContext as MainViewModel;

            if (MainVm is null)
                throw new InvalidOperationException("MainViewModel can't be null.");

            MainVm.DropCommand = new RelayCommand<DragEventArgs>(HandleDrop);
            MainVm.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(MainViewModel.CurrentMediaView))
                    Dispatcher.UIThread.Post(SubscribeToMediaPlayerErrors, DispatcherPriority.Loaded);
            };

            // Subscribe to the initial MediaPlayerControl (created before PropertyChanged was wired)
            Dispatcher.UIThread.Post(SubscribeToMediaPlayerErrors, DispatcherPriority.Loaded);

            base.OnAttachedToVisualTree(e);
        }

        private void SubscribeToMediaPlayerErrors()
        {
            var player = this.GetVisualDescendants().OfType<MediaPlayerControl>().FirstOrDefault();
            if (player == _subscribedPlayer)
                return;

            if (_subscribedPlayer != null)
                _subscribedPlayer.ErrorOccurred -= OnMediaError;

            _subscribedPlayer = player;
            if (_subscribedPlayer != null)
                _subscribedPlayer.ErrorOccurred += OnMediaError;
        }

        private void OnMediaError(object? sender, MediaPlayerControlErrorEventArgs e)
        {
            e.Handled = true;
            ShowErrorToast(e.Message ?? "Unknown media error");
        }

        private void ShowErrorToast(string message)
        {
            ErrorToastText.Text = message;
            ErrorToast.IsVisible = true;
            ErrorToast.Opacity = 1;

            _toastTimer?.Stop();
            _toastTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            _toastTimer.Tick += (_, _) =>
            {
                _toastTimer.Stop();
                ErrorToast.Opacity = 0;
                var hideTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(300) };
                hideTimer.Tick += (_, _) =>
                {
                    hideTimer.Stop();
                    ErrorToast.IsVisible = false;
                };
                hideTimer.Start();
            };
            _toastTimer.Start();
        }

        private void HandleDrop(DragEventArgs? e)
        {
            var files = e?.DataTransfer.TryGetFiles()?.FirstOrDefault();

            if (files is null || MainVm is null)
                return;

            MainVm.SetSource(new UriSource(files.Path));

        }

        private async void Load_Click(object? _, RoutedEventArgs __)
        {
            var storageProver = TopLevel.GetTopLevel(this)?.StorageProvider;
            if (storageProver is null || MainVm is null)
                return;

            var files = await storageProver.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                AllowMultiple = false
            });

            if (files.Count != 1)
                return;
            if (files[0].Path is not { } path)
                return;

            MainVm.Source = new StorageFileSource(files[0]);
        }

        private void InputElement_OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            MainVm?.DragMoveCommand?.Execute(e);
        }

        private async void Load_Uri_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var dialog = new ContentDialog()
            {
                PrimaryButtonText = "Open",
                SecondaryButtonText = "Cancel",
                Title = "Open URL"
            };

            var input = new TextBox()
            {
                PlaceholderText = "Enter Url"
            };

            dialog.Content = input;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (string.IsNullOrWhiteSpace(input.Text) || MainVm == null)
                    return;

                try
                {
                    MainVm.Source = new UriSource(input.Text);
                }
                catch (UriFormatException ex)
                {
                    ShowErrorToast(ex.Message);
                }
            }
        }
    }
}
