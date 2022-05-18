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
        void AddAirportAsync(Airport airport);
        void UpdateAirportAsync(Airport airport);
        void DeleteAirportAsync(string icao);
    }
}
