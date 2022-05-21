using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Solari.App.ViewModels
{
    public class AddFlightViewModel : ObservableRecipient, INotifyPropertyChanged
    {
        private readonly IFlightService _FlightSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        public Flight NewFlight { get; set; } = new() { DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now };

        public AddFlightViewModel(IFlightService flightService)
        {
            _FlightSerivce = flightService;
        }

        private ICommand _AddFlightCommand;
        public ICommand AddFlightCommand
        {
            get
            {
                if (_AddFlightCommand == null)
                {
                    _AddFlightCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to add flight.
                            await _FlightSerivce.AddFlightAsync(NewFlight);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Flight successfully added.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _AddFlightCommand;
            }
        }

        private DateTimeOffset _DepartureDate = DateTimeOffset.Now;
        public DateTimeOffset DepartureDate
        {
            get => _DepartureDate;
            set
            {
                _ = SetProperty(ref _DepartureDate, value);
                SetFlightDepartureTime();
            }
        }

        private TimeSpan _DepartureTime = DateTime.Now.TimeOfDay;
        public TimeSpan DepartureTime
        {
            get => _DepartureTime;
            set
            {
                _ = SetProperty(ref _DepartureTime, value);
                SetFlightDepartureTime();
            }
        }

        private void SetFlightDepartureTime()
        {
            DateTime newDepartureTime = new(
                DepartureDate.Year, DepartureDate.Month, DepartureDate.Day,
                DepartureTime.Hours, DepartureTime.Minutes, DepartureTime.Seconds);

            if (newDepartureTime < NewFlight.ArrivalTime)
                NewFlight.DepartureTime = newDepartureTime;
            else
                ErrorDialogService.ShowAsync("The departure time must be earlier than the arrival time.");
        }

        private DateTimeOffset _ArrivalDate = DateTimeOffset.Now;
        public DateTimeOffset ArrivalDate
        {
            get => _ArrivalDate;
            set
            {
                _ = SetProperty(ref _ArrivalDate, value);
                SetFlightArrivalTime();
            }
        }

        private TimeSpan _ArrivalTime = DateTime.Now.TimeOfDay;
        public TimeSpan ArrivalTime
        {
            get => _ArrivalTime;
            set
            {
                _ = SetProperty(ref _ArrivalTime, value);
                SetFlightArrivalTime();
            }
        }

        private void SetFlightArrivalTime()
        {
            DateTime newArrivalTime = new(
                ArrivalDate.Year, ArrivalDate.Month, ArrivalDate.Day,
                ArrivalTime.Hours, ArrivalTime.Minutes, ArrivalTime.Seconds);

            if (newArrivalTime > NewFlight.DepartureTime)
                NewFlight.ArrivalTime = newArrivalTime;
            else
                ErrorDialogService.ShowAsync("The arrival time must be later than the departure time.");
        }
    }
}
