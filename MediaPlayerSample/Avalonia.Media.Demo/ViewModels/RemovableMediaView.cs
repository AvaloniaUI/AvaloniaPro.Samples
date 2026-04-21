using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalonia.Media.Demo.ViewModels
{
    public partial class RemovableMediaView : ViewModelBase
    {
        [ObservableProperty] private MediaSource? _source;
    }
}
