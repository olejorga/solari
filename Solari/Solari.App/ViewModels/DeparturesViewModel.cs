using CommunityToolkit.Mvvm.ComponentModel;
using Solari.App.Contracts.ViewModels;
using Solari.App.Core.Contracts.Services;
using Solari.App.Helpers;
using Solari.Data.Access.Models;
using System.Collections.ObjectModel;
using Windows.Storage;

namespace Solari.App.ViewModels
{
    public class DeparturesViewModel : ObservableRecipient, INavigationAware
    {
        private readonly IAirportService _airportService;

        public ObservableCollection<Flight> Source { get; } = new ObservableCollection<Flight>();

        private Airport _selectedairport;
        public Airport SelectedAirport
        {
            get => _selectedairport;
            set => SetProperty(ref _selectedairport, value);
        }

        public DeparturesViewModel(IAirportService airportService)
        {
            _airportService = airportService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            Source.Clear();

            string icao = await ApplicationData.Current.LocalSettings.ReadAsync<string>("SelectedAirportIcao");

            if (string.IsNullOrEmpty(icao)) icao = "ENGM";

            // Replace this with your actual data
            SelectedAirport = await _airportService.GetAirportAsync(icao);

            foreach (Flight flight in SelectedAirport.DepartingFlights)
            {
                Source.Add(flight);
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
