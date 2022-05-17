using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.Data.Access.Contracts.Repositories
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
