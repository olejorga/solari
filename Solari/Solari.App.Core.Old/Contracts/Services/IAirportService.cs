using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.App.Contracts.Services
{
    public interface IAirportService
    {
        Task<IEnumerable<Airport>> GetAirportsAsync();
        Task<IEnumerable<Airport>> SearchAirportsAsync(string query);
        Task<Airport> GetAirportAsync(string icao);
        Task<bool> AddAirportAsync(Airport airport);
        Task<bool> UpdateAirportAsync(Airport airport);
        Task<bool> DeleteAirportAsync(string icao);
    }
}
