using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.App.Contracts.Services
{
    public interface IFlightService
    {
        Task<IEnumerable<Flight>> GetFlightsAsync();
        Task<IEnumerable<Flight>> SearchFlightsAsync(string query);
        Task<Flight> GetFlightAsync(string flightNumber);
        Task<Flight> AddFlightAsync(Flight flight);
        Task<Flight> UpdateFlightAsync(Flight flight);
        Task<Flight> DeleteFlightAsync(string flightNumber);
    }
}
