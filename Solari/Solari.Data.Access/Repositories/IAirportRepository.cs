using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.Data.Access.Repositories
{
    public interface IAirportRepository
    {
        Task<IEnumerable<Airport>> GetAirportsAsync();
        Task<Airport> GetAirportAsync(string icao);
        Task<Airport> AddAirportAsync(Airport airport);
        Task<Airport> UpdateAirportAsync(Airport airport);
        Task<Boolean> DeleteAirportAsync(string icao);
    }
}
