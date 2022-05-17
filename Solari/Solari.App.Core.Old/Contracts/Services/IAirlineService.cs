using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.App.Core.Contracts.Services
{
    public interface IAirlineService
    {
        Task<Airline> GetAirlineAsync(string icao);
        Task<bool> AddAirlineAsync(Airline airline);
        Task<bool> UpdateAirlineAsync(Airline airline);
        Task<bool> DeleteAirlineAsync(string icao);
    }
}
