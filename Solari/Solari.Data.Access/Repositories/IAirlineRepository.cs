using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.Data.Access.Repositories
{
    public interface IAirlineRepository
    {
        Task<IEnumerable<Airline>> GetAirlinesAsync();
        Task<Airline> GetAirlineAsync(string icao);
        Task<Airline> AddAirlineAsync(Airline airline);
        Task<Airline> UpdateAirlineAsync(Airline airline);
        Task<Airline> DeleteAirlineAsync(string icao);
    }
}
