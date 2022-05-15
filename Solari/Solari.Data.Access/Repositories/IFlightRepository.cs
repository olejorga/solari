using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.Data.Access.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task<Flight> GetFlightAsync(string flightNumber);
        Task<Flight> AddFlightAsync(Flight flight);
        Task<Flight> UpdateFlightAsync(Flight flight);
        Task<Flight> DeleteFlightAsync(string flightNumber);
    }
}
