using CommunityToolkit.Mvvm.ComponentModel;
using Solari.App.Contracts.Services;
using Solari.App.Contracts.ViewModels;
using Solari.App.Core.Contracts.Services;
using Solari.App.Helpers;
using Solari.Data.Access.Models;
using System;
using System.Collections.ObjectModel;
using Windows.Storage;

namespace Solari.App.ViewModels
{
    public class DeparturesViewModel : ObservableRecipient, INavigationAware
    {
        private readonly IAirportService _airportService;

        public INavigationService NavigationService { get; }

        public IDialogService ErrorDialogService { get; set; }

        public ObservableCollection<Flight> Source { get; } = new ObservableCollection<Flight>();

        private Airport _selectedAirport;
        public Airport SelectedAirport
        {
            get => _selectedAirport;
            set => SetProperty(ref _selectedAirport, value);
        }

        public DeparturesViewModel(IAirportService airportService, INavigationService navigationService)
        {
            _airportService = airportService;
            NavigationService = navigationService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            Source.Clear();

            string icao = await ApplicationData.Current.LocalSettings.ReadAsync<string>("SelectedAirportIcao");

            // Default selected airport to ..., if non is already set.
            if (string.IsNullOrEmpty(icao))
            {
                icao = "ENGM";
            }

            // Try to fetch the airport
            try
            {
                SelectedAirport = await _airportService.GetAirportAsync(icao);

                // Add all departing flights at the airport to the view model.
                foreach (Flight flight in SelectedAirport.DepartingFlights)
                {
                    Source.Add(flight);
                }
            }
            catch (Exception exception)
            {
                _ = ErrorDialogService.ShowAsync(exception.Message);

                // Send user back to start page.
                _ = NavigationService.NavigateTo("Solari.App.ViewModels.LandingPage");
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
