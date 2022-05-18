using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.App.Core.Contracts.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task<IEnumerable<Flight>> SearchFlightsAsync(string query);
        Task<Flight> GetFlightAsync(string flightNumber);
        void AddFlightAsync(Flight flight);
        void UpdateFlightAsync(Flight flight);
        void DeleteFlightAsync(string flightNumber);
    }
}
