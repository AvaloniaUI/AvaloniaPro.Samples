using Avalonia.Charts.Demo.ViewModels.Charts;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Avalonia.Charts.Demo.Views.Charts;
    public partial class AccessibilityPage : UserControl
    {
        public AccessibilityPage()
        {
            InitializeComponent();
            DataContext = new AccessibilityViewModel();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

