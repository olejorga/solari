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
        private readonly IFlightService _flightSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        public Flight NewFlight { get; set; } = new() { DepartureTime = DateTime.Now, ArrivalTime = DateTime.Now };

        public AddFlightViewModel(IFlightService flightService)
        {
            _flightSerivce = flightService;
        }

        private ICommand _addFlightCommand;
        public ICommand AddFlightCommand
        {
            get
            {
                if (_addFlightCommand == null)
                {
                    _addFlightCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to add flight.
                            await _flightSerivce.AddFlightAsync(NewFlight);

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

                return _addFlightCommand;
            }
        }

        private DateTimeOffset _departureDate = DateTimeOffset.Now;
        public DateTimeOffset DepartureDate
        {
            get => _departureDate;
            set
            {
                _ = SetProperty(ref _departureDate, value);
                SetFlightDepartureTime();
            }
        }

        private TimeSpan _departureTime = DateTime.Now.TimeOfDay;
        public TimeSpan DepartureTime
        {
            get => _departureTime;
            set
            {
                _ = SetProperty(ref _departureTime, value);
                SetFlightDepartureTime();
            }
        }

        private void SetFlightDepartureTime()
        {
            DateTime newDepartureTime = new(
                DepartureDate.Year, DepartureDate.Month, DepartureDate.Day,
                DepartureTime.Hours, DepartureTime.Minutes, DepartureTime.Seconds);

            if (newDepartureTime < NewFlight.ArrivalTime)
            {
                NewFlight.DepartureTime = newDepartureTime;
            }
            else
            {
                _ = ErrorDialogService.ShowAsync("The departure time must be earlier than the arrival time.");
            }
        }

        private DateTimeOffset _arrivalDate = DateTimeOffset.Now;
        public DateTimeOffset ArrivalDate
        {
            get => _arrivalDate;
            set
            {
                _ = SetProperty(ref _arrivalDate, value);
                SetFlightArrivalTime();
            }
        }

        private TimeSpan _arrivalTime = DateTime.Now.TimeOfDay;
        public TimeSpan ArrivalTime
        {
            get => _arrivalTime;
            set
            {
                _ = SetProperty(ref _arrivalTime, value);
                SetFlightArrivalTime();
            }
        }

        private void SetFlightArrivalTime()
        {
            DateTime newArrivalTime = new(
                ArrivalDate.Year, ArrivalDate.Month, ArrivalDate.Day,
                ArrivalTime.Hours, ArrivalTime.Minutes, ArrivalTime.Seconds);

            if (newArrivalTime > NewFlight.DepartureTime)
            {
                NewFlight.ArrivalTime = newArrivalTime;
            }
            else
            {
                _ = ErrorDialogService.ShowAsync("The arrival time must be later than the departure time.");
            }
        }
    }
}
