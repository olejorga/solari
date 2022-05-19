using System;
using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;

namespace Solari.App.ViewModels
{
    public class AddAirlineViewModel : ObservableRecipient
    {
        private readonly IAirlineService _AirlineSerivce;

        public Airline NewAirline { get; set; } = new();

        public AddAirlineViewModel(IAirlineService airlineService)
        {
            _AirlineSerivce = airlineService;
            NewAirline.Name = "Norwegian";
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
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine(exception.Message);
                        }
                    });
                }

                return _AddAirlineCommand;
            }
        }
    }
}
