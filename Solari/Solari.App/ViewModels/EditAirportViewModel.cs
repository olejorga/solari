using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;
using System;
using System.Windows.Input;

namespace Solari.App.ViewModels
{
    public class EditAirportViewModel : ObservableRecipient
    {
        private readonly IAirportService _AirportSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        private Airport _updatedAirport;
        public Airport UpdatedAirport
        {
            get => _updatedAirport;
            set => SetProperty(ref _updatedAirport, value);
        }

        private string _userInputtedIcao;
        public string UserInputtedIcao
        {
            get => _userInputtedIcao;
            set => SetProperty(ref _userInputtedIcao, value);
        }

        public EditAirportViewModel(IAirportService airportService)
        {
            _AirportSerivce = airportService;
        }

        private ICommand _EditAirportCommand;
        public ICommand EditAirportCommand
        {
            get
            {
                if (_EditAirportCommand == null)
                {
                    _EditAirportCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to edit airport.
                            await _AirportSerivce.UpdateAirportAsync(UpdatedAirport);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Airport successfully edited.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _EditAirportCommand;
            }
        }

        private ICommand _SearchAirportCommand;
        public ICommand SearchAirportCommand
        {
            get
            {
                if (_SearchAirportCommand == null)
                {
                    _SearchAirportCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to get airport.
                            UpdatedAirport = await _AirportSerivce.GetAirportAsync(UserInputtedIcao);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Airport found.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _SearchAirportCommand;
            }
        }
    }
}
