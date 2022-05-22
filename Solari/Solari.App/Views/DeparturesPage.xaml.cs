using CommunityToolkit.Mvvm.DependencyInjection;

using Microsoft.UI.Xaml.Controls;
using Solari.App.Contracts.Services;
using Solari.App.Helpers;
using Solari.App.ViewModels;
using Solari.Data.Access.Models;
using Windows.Storage;

namespace Solari.App.Views
{
    public sealed partial class DeparturesPage : Page
    {
        public DeparturesViewModel ViewModel { get; }

        private readonly INavigationService _navigationService;

        public DeparturesPage()
        {
            ViewModel = Ioc.Default.GetService<DeparturesViewModel>();
            _navigationService = Ioc.Default.GetService<INavigationService>();

            InitializeComponent();
        }

        private async void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Flight flight = (Flight)DataGrid.SelectedItem;

            await ApplicationData.Current.LocalSettings.SaveAsync("SelectedFlightNumber", flight.FlightNumber);

            _ = _navigationService.NavigateTo("Solari.App.ViewModels.FlightViewModel");
        }
    }
}
