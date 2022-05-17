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
        void AddAirlineAsync(Airline airline);
        void UpdateAirlineAsync(Airline airline);
        void DeleteAirlineAsync(string icao);
    }
}
