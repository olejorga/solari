using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;
using System;
using System.Windows.Input;

namespace Solari.App.ViewModels
{
    public class AddAirportViewModel : ObservableRecipient
    {
        private readonly IAirportService _AirportSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        public Airport NewAirport { get; set; } = new();

        public AddAirportViewModel(IAirportService airportService)
        {
            _AirportSerivce = airportService;
        }

        private ICommand _AddAirportCommand;
        public ICommand AddAirportCommand
        {
            get
            {
                if (_AddAirportCommand == null)
                {
                    _AddAirportCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            // Try to add airport.
                            await _AirportSerivce.AddAirportAsync(NewAirport);

                            // If successful, create success dialog.
                            _ = await InfoDialogService.ShowAsync("Airport successfully added.");
                        }
                        catch (Exception exception)
                        {
                            // If unsuccessful, create error dialog, with error message from service.
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _AddAirportCommand;
            }
        }
    }
}
