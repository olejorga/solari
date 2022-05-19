using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class AddFlightPage : Page
    {
        public AddFlightViewModel ViewModel { get; }

        public AddFlightPage()
        {
            ViewModel = Ioc.Default.GetService<AddFlightViewModel>();
            InitializeComponent();
        }
    }
}
