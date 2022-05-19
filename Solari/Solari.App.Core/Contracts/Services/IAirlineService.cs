using Solari.Data.Access.Models;
using System.Threading.Tasks;

namespace Solari.App.Core.Contracts.Services
{
    public interface IAirlineService
    {
        Task<Airline> GetAirlineAsync(string icao);
        Task AddAirlineAsync(Airline airline);
        Task UpdateAirlineAsync(Airline airline);
        Task DeleteAirlineAsync(string icao);
    }
}
