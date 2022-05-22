using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.App.Core.Contracts.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task<Flight> GetFlightAsync(string flightNumber);
        Task AddFlightAsync(Flight flight);
        Task UpdateFlightAsync(Flight flight);
        Task DeleteFlightAsync(string flightNumber);
    }
}
