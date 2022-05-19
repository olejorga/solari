using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;

using Solari.App.ViewModels;

namespace Solari.App.Views
{
    public sealed partial class EditAirportPage : Page
    {
        public EditAirportViewModel ViewModel { get; }

        public EditAirportPage()
        {
            ViewModel = Ioc.Default.GetService<EditAirportViewModel>();
            InitializeComponent();
        }
    }
}
