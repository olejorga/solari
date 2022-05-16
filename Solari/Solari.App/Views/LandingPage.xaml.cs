using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class LandingPage : Page
    {
        public LandingViewModel ViewModel { get; }

        public LandingPage()
        {
            ViewModel = Ioc.Default.GetService<LandingViewModel>();
            InitializeComponent();
        }
    }
}
