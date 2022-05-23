using CommunityToolkit.Mvvm.ComponentModel;
using Solari.App.Contracts.ViewModels;
using Solari.App.Core.Contracts.Services;
using Solari.App.Helpers;
using Solari.Data.Access.Models;
using Windows.Storage;

namespace Solari.App.ViewModels
{
    public class FlightViewModel : ObservableRecipient, INavigationAware
    {
        private readonly IFlightService _flightService;

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

        public FlightViewModel(IFlightService flightService)
        {
            _flightService = flightService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            string flightNumber = await ApplicationData.Current.LocalSettings.ReadAsync<string>("SelectedFlightNumber");

            if (string.IsNullOrEmpty(flightNumber))
            {
                //
            }

            SelectedFlight = await _flightService.GetFlightAsync(flightNumber);
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
