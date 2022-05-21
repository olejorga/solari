using CommunityToolkit.Mvvm.ComponentModel;
using Solari.App.Contracts.ViewModels;
using Solari.App.Core.Contracts.Services;
using Solari.Data.Access.Models;
using System.Collections.ObjectModel;

namespace Solari.App.ViewModels
{
    public class DeparturesViewModel : ObservableRecipient, INavigationAware
    {
        private readonly IAirportService _airportService;

        public ObservableCollection<Flight> Source { get; } = new ObservableCollection<Flight>();

        public DeparturesViewModel(IAirportService airportService)
        {
            _airportService = airportService;
        }

        public async void OnNavigatedTo(object parameter)
        {
            Source.Clear();

            // Replace this with your actual data
            Airport airport = await _airportService.GetAirportAsync("ENGM");

            foreach (Flight flight in airport.DepartingFlights)
            {
                Source.Add(flight);
            }
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
