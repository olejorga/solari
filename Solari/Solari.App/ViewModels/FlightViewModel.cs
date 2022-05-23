using CommunityToolkit.Mvvm.ComponentModel;
using Solari.App.Contracts.Services;
using Solari.App.Contracts.ViewModels;
using Solari.App.Core.Contracts.Services;
using Solari.App.Helpers;
using Solari.Data.Access.Models;
using System;
using Windows.Storage;

namespace Solari.App.ViewModels
{
    public class FlightViewModel : ObservableRecipient, INavigationAware
    {
        private readonly IFlightService _flightService;

        private readonly INavigationService _navigationService;

        public IDialogService ErrorDialogService { get; set; }

        private Flight _selectedFlight;
        public Flight SelectedFlight
        {
            get => _selectedFlight;
            set
            {
                _ = SetProperty(ref _selectedFlight, value);
                SetFlightTime();
            }
        }

        private int _flightTime;
        public int FlightTime
        {
            get => _flightTime;
            set => SetProperty(ref _flightTime, value);
        }

        public FlightViewModel(IFlightService flightService, INavigationService navigationService)
        {
            _flightService = flightService;
            _navigationService = navigationService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            string flightNumber = await ApplicationData.Current.LocalSettings.ReadAsync<string>("SelectedFlightNumber");

            if (string.IsNullOrEmpty(flightNumber))
            {
                _ = ErrorDialogService.ShowAsync("Something went wrong while transferring data.");

                // Send user back to start page.
                _ = _navigationService.NavigateTo("Solari.App.ViewModels.LandingViewModel");
            }

            // Try to get flight
            try
            {
                SelectedFlight = await _flightService.GetFlightAsync(flightNumber);
            }
            catch (Exception exception)
            {
                _ = ErrorDialogService.ShowAsync(exception.Message);
            }
        }

        public void OnNavigatedFrom()
        {
        }

        private void SetFlightTime()
        {
            FlightTime = (int)SelectedFlight.ArrivalTime.Subtract(SelectedFlight.DepartureTime).TotalMinutes;
        }
    }
}
