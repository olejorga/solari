using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;
using System;
using System.Windows.Input;

namespace Solari.App.ViewModels
{
    public class AddAirlineViewModel : ObservableRecipient
    {
        private readonly IAirlineService _airlineSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        public Airline NewAirline { get; set; } = new();

        public AddAirlineViewModel(IAirlineService airlineService)
        {
            _airlineSerivce = airlineService;
        }

        private ICommand _addAirlineCommand;
        public ICommand AddAirlineCommand
        {
            get
            {
                if (_addAirlineCommand == null)
                {
                    _addAirlineCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to add airline.
                            await _airlineSerivce.AddAirlineAsync(NewAirline);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Airline successfully added.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _addAirlineCommand;
            }
        }
    }
}
