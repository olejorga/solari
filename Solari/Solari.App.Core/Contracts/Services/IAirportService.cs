using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.App.Core.Contracts.Services
{
    public interface IAirportService
    {
        Task<IEnumerable<Airport>> GetAirportsAsync();
        Task<IEnumerable<Airport>> SearchAirportsAsync(string query);
        Task<Airport> GetAirportAsync(string icao);
        Task AddAirportAsync(Airport airport);
        Task UpdateAirportAsync(Airport airport);
        Task DeleteAirportAsync(string icao);
    }
}
