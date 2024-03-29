﻿using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.Data.Access.Contracts.Repositories
{
    public interface IAirportRepository
    {
        Task<IEnumerable<Airport>> GetAirportsAsync();
        Task<Airport> GetAirportAsync(string icao);
        Task<Airport> AddAirportAsync(Airport airport);
        Task<Airport> UpdateAirportAsync(Airport airport);
        Task<Airport> DeleteAirportAsync(string icao);
    }
}
