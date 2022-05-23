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
        private readonly IAirlineService _airlineSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        public IDialogService ConfirmationDialogService { get; set; }

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
            _airlineSerivce = airlineService;
        }

        private ICommand _editAirlineCommand;
        public ICommand EditAirlineCommand
        {
            get
            {
                if (_editAirlineCommand == null)
                {
                    _editAirlineCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to edit airline.
                            await _airlineSerivce.UpdateAirlineAsync(UpdatedAirline);

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

                return _editAirlineCommand;
            }
        }

        private ICommand _searchAirlineCommand;
        public ICommand SearchAirlineCommand
        {
            get
            {
                if (_searchAirlineCommand == null)
                {
                    _searchAirlineCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to get airline.
                            UpdatedAirline = await _airlineSerivce.GetAirlineAsync(UserInputtedIcao);

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

                return _searchAirlineCommand;
            }
        }

        private ICommand _deleteAirlineCommand;
        public ICommand DeleteAirlineCommand
        {
            get
            {
                if (_deleteAirlineCommand == null)
                {
                    _deleteAirlineCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Ask for confirmation.
                            DialogResult answer = await ConfirmationDialogService.ShowAsync("Are you sure you want to delete this airline?");

                            // Check confirmation answer. If not yes, stop command execution.
                            if (answer != DialogResult.Primary)
                            {
                                return;
                            }

                            // Try to delete airline.
                            await _airlineSerivce.DeleteAirlineAsync(UpdatedAirline.Icao);

                            // Clear local airline object, as it is deleted.
                            UpdatedAirline = null;
                            UserInputtedIcao = null;

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Airline deleted.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _deleteAirlineCommand;
            }
        }
    }
}
