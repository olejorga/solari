using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;
using System;
using System.Windows.Input;

namespace Solari.App.ViewModels
{
    public class EditFlightViewModel : ObservableRecipient
    {
        private readonly IFlightService _flightSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        public IDialogService ConfirmationDialogService { get; set; }

        public bool IsInitializing { get; set; }

        private Flight _updatedFlight;
        public Flight UpdatedFlight
        {
            get => _updatedFlight;
            set => SetProperty(ref _updatedFlight, value);
        }

        private string _userInputtedFlightNumber;
        public string UserInputtedFlightNumber
        {
            get => _userInputtedFlightNumber;
            set => SetProperty(ref _userInputtedFlightNumber, value);
        }

        public EditFlightViewModel(IFlightService flightService)
        {
            _flightSerivce = flightService;
        }

        private ICommand _editFlightCommand;
        public ICommand EditFlightCommand
        {
            get
            {
                if (_editFlightCommand == null)
                {
                    _editFlightCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to edit flight.
                            await _flightSerivce.UpdateFlightAsync(UpdatedFlight);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Flight successfully edited.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _editFlightCommand;
            }
        }

        private ICommand _searchFlightCommand;
        public ICommand SearchFlightCommand
        {
            get
            {
                if (_searchFlightCommand == null)
                {
                    _searchFlightCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Initial flight load.
                            IsInitializing = true;

                            // Try to get flight.
                            UpdatedFlight = await _flightSerivce.GetFlightAsync(UserInputtedFlightNumber);

                            // Sync local date and time fields with flight.
                            DepartureDate = UpdatedFlight.DepartureTime;
                            DepartureTime = UpdatedFlight.DepartureTime.TimeOfDay;
                            ArrivalDate = UpdatedFlight.ArrivalTime;
                            ArrivalTime = UpdatedFlight.ArrivalTime.TimeOfDay;

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Flight found.");

                            // End of initial flight load.
                            IsInitializing = false;
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _searchFlightCommand;
            }
        }

        private ICommand _deleteFlightCommand;
        public ICommand DeleteFlightCommand
        {
            get
            {
                if (_deleteFlightCommand == null)
                {
                    _deleteFlightCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Ask for confirmation.
                            DialogResult answer = await ConfirmationDialogService.ShowAsync("Are you sure you want to delete this flight?");

                            // Check confirmation answer. If not yes, stop command execution.
                            if (answer != DialogResult.Primary)
                            {
                                return;
                            }

                            // Try to delete flight.
                            await _flightSerivce.DeleteFlightAsync(UpdatedFlight.FlightNumber);

                            // Clear local flight object, as it is deleted.
                            UpdatedFlight = null;
                            UserInputtedFlightNumber = null;

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Flight deleted.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _deleteFlightCommand;
            }
        }

        private DateTimeOffset _departureDate;
        public DateTimeOffset DepartureDate
        {
            get => _departureDate;
            set
            {
                _ = SetProperty(ref _departureDate, value);

                if (!IsInitializing)
                {
                    SetFlightDepartureTime();
                }
            }
        }

        private TimeSpan _departureTime;
        public TimeSpan DepartureTime
        {
            get => _departureTime;
            set
            {
                _ = SetProperty(ref _departureTime, value);

                if (!IsInitializing)
                {
                    SetFlightDepartureTime();
                }
            }
        }

        private void SetFlightDepartureTime()
        {
            if (UpdatedFlight != null)
            {
                DateTime newDepartureTime = new(
                DepartureDate.Year, DepartureDate.Month, DepartureDate.Day,
                DepartureTime.Hours, DepartureTime.Minutes, DepartureTime.Seconds);

                if (newDepartureTime < UpdatedFlight.ArrivalTime)
                {
                    UpdatedFlight.DepartureTime = newDepartureTime;
                }
                else
                {
                    _ = ErrorDialogService.ShowAsync("The departure time must be earlier than the arrival time.");
                }
            }
        }

        private DateTimeOffset _arrivalDate;
        public DateTimeOffset ArrivalDate
        {
            get => _arrivalDate;
            set
            {
                _ = SetProperty(ref _arrivalDate, value);

                if (!IsInitializing)
                {
                    SetFlightArrivalTime();
                }
            }
        }

        private TimeSpan _arrivalTime;
        public TimeSpan ArrivalTime
        {
            get => _arrivalTime;
            set
            {
                _ = SetProperty(ref _arrivalTime, value);

                if (!IsInitializing)
                {
                    SetFlightArrivalTime();
                }
            }
        }

        private void SetFlightArrivalTime()
        {
            if (UpdatedFlight != null)
            {
                DateTime newArrivalTime = new(
                ArrivalDate.Year, ArrivalDate.Month, ArrivalDate.Day,
                ArrivalTime.Hours, ArrivalTime.Minutes, ArrivalTime.Seconds);

                if (newArrivalTime > UpdatedFlight.DepartureTime)
                {
                    UpdatedFlight.ArrivalTime = newArrivalTime;
                }
                else
                {
                    _ = ErrorDialogService.ShowAsync("The arrival time must be later than the departure time.");
                }
            }
        }
    }
}
