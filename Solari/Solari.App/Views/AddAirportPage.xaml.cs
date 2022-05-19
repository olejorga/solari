using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class AddAirportPage : Page
    {
        public AddAirportViewModel ViewModel { get; }

        public AddAirportPage()
        {
            ViewModel = Ioc.Default.GetService<AddAirportViewModel>();
            InitializeComponent();
        }
    }
}
