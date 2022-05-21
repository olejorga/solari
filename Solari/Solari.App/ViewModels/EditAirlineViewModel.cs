using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;
using System;
using System.Windows.Input;

namespace Solari.App.ViewModels
{
    public class EditAirlineViewModel : ObservableRecipient
    {
        private readonly IAirlineService _AirlineSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        private Airline _updatedAirline;
        public Airline UpdatedAirline
        {
            get => _updatedAirline;
            set => SetProperty(ref _updatedAirline, value);
        }

        private string _userInputtedIcao;
        public string UserInputtedIcao
        {
            get => _userInputtedIcao;
            set => SetProperty(ref _userInputtedIcao, value);
        }

        public EditAirlineViewModel(IAirlineService airlineService)
        {
            _AirlineSerivce = airlineService;
        }

        private ICommand _EditAirlineCommand;
        public ICommand EditAirlineCommand
        {
            get
            {
                if (_EditAirlineCommand == null)
                {
                    _EditAirlineCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to edit airline.
                            await _AirlineSerivce.UpdateAirlineAsync(UpdatedAirline);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Airline successfully edited.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _EditAirlineCommand;
            }
        }

        private ICommand _SearchAirlineCommand;
        public ICommand SearchAirlineCommand
        {
            get
            {
                if (_SearchAirlineCommand == null)
                {
                    _SearchAirlineCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to get airline.
                            UpdatedAirline = await _AirlineSerivce.GetAirlineAsync(UserInputtedIcao);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Airline found.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _SearchAirlineCommand;
            }
        }
    }
}
