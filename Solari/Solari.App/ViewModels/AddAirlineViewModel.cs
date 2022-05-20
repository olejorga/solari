using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.App.Services;
using Solari.Data.Access.Models;

namespace Solari.App.ViewModels
{
    public class AddAirlineViewModel : ObservableRecipient
    {
        private readonly IAirlineService _AirlineSerivce;

        public IDialogService ErrorDialogService { get; set; }

        public IDialogService InfoDialogService { get; set; }

        public Airline NewAirline { get; set; } = new();

        public AddAirlineViewModel(IAirlineService airlineService)
        {
            _AirlineSerivce = airlineService;
        }

        private ICommand _AddAirlineCommand;
        public ICommand AddAirlineCommand
        {
            get
            {
                if (_AddAirlineCommand == null)
                {
                    _AddAirlineCommand = new RelayCommand(async () =>
                    {
                        try
                        {
                            await _AirlineSerivce.AddAirlineAsync(NewAirline);
                            _ = await InfoDialogService.ShowAsync("Airline successfully added.");
                        }
                        catch (Exception exception)
                        {
                            _ = await ErrorDialogService.ShowAsync(exception.Message);
                        }
                    });
                }

                return _AddAirlineCommand;
            }
        }
    }
}
