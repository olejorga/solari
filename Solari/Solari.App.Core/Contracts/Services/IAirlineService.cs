using Solari.Data.Access.Models;
using System.Threading.Tasks;

namespace Solari.App.Core.Contracts.Services
{
    public interface IAirlineService
    {
        Task<Airline> GetAirlineAsync(string icao);
        void AddAirlineAsync(Airline airline);
        void UpdateAirlineAsync(Airline airline);
        void DeleteAirlineAsync(string icao);
    }
}
