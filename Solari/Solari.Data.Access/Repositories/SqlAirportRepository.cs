using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Exceptions;
using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.Data.Access.Repositories
{
    /// <summary>
    /// An abstraction of the airports SQL table, 
    /// built with EF Core 5 and designed with CRUD in mind.
    /// </summary>
    public class SqlAirportRepository : IAirportRepository
    {
        private readonly SolariContext DbContext;

        public SqlAirportRepository(SolariContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Gets all airports in repository.
        /// </summary>
        /// <returns>All airports in repository.</returns>
        /// <exception cref="EntitiesNotFoundException">No airports were found.</exception>
        public async Task<IEnumerable<Airport>> GetAirportsAsync()
        {
            // Get all airports.
            var airports = await DbContext.Airports
                .Include(e => e.DepartingFlights)
                .Include(e => e.ArrivingFlights)
                .ToListAsync();

            // If no airports were found, throw exception.
            if (airports.Count == 0)
                throw new EntitiesNotFoundException("No airports were found!");

            // If airports were found.
            return airports;
        }

        /// <summary>
        /// Gets airport by ICAO code.
        /// </summary>
        /// <param name="icao">The airports four letter ICAO identifier.</param>
        /// <returns>Airport matching the ICAO code.</returns>
        /// <exception cref="EntityNotFoundException">No airport were found.</exception>
        public async Task<Airport> GetAirportAsync(string icao)
        {
            // Enforcing that the ICAO code is uppercase.
            icao = icao.ToUpper();

            // Get airport by ICAO code.
            var airport = await DbContext.Airports
                .Include(e => e.DepartingFlights)
                .Include(e => e.ArrivingFlights)
                .FirstOrDefaultAsync(e => e.Icao == icao.ToUpper());

            // If airport is not found, throw exception.
            if (airport == null)
                throw new EntityNotFoundException($"Airport with ICAO = {icao} not found!");

            // If airport is found, return airport.
            return airport;
        }

        /// <summary>
        /// Adds an airport to the repository.
        /// </summary>
        /// <param name="airport">A airport object.</param>
        /// <returns>The added airport entry in the repository.</returns>
        /// <exception cref="EntityAlreadyExistsException">Airport already exists.</exception>
        public async Task<Airport> AddAirportAsync(Airport airport)
        {
            try
            {
                // Check if an airport with the same ICAO code exists.
                // Throws "EntityNotFoundException" if airport does not already exist.
                await GetAirportAsync(airport.Icao);

                // If the airport does already exist, throw exception.
                throw new EntityAlreadyExistsException("Airport already exists!");
            }
            catch (EntityNotFoundException)
            {
                // If the airport does not exist, create airport.
                await DbContext.Airports.AddAsync(airport);

                // Update database.
                await DbContext.SaveChangesAsync();

                // Get and return the created airport.
                return await GetAirportAsync(airport.Icao);
            }
        }

        /// <summary>
        /// Updates an airport in the repository.
        /// </summary>
        /// <param name="airport">A updated airport object.</param>
        /// <returns>The updated airport entry in the repository.</returns>
        /// <exception cref="EntityNotFoundException">No airport were found.</exception>
        public async Task<Airport> UpdateAirportAsync(Airport airport)
        {
            // Get the airport the user is trying to update exists.
            // Throws "EntityNotFoundException" if airport does not already exist.
            var airportToUpdate = await GetAirportAsync(airport.Icao);

            // If the airport exists, update airport.
            airportToUpdate.Icao = airport.Icao;
            airportToUpdate.Iata = airport.Iata;
            airportToUpdate.Name = airport.Name;
            airportToUpdate.City = airport.City;

            // Update database.
            await DbContext.SaveChangesAsync();

            // Return the updated airport.
            return airportToUpdate;
        }

        /// <summary>
        /// Delete an airport from the repository.
        /// </summary>
        /// <param name="icao">The airports four letter ICAO identifier.</param>
        /// <returns>The deleted airport entry in the repository.</returns>
        /// <exception cref="EntityNotFoundException">No airport were found.</exception>
        public async Task<Airport> DeleteAirportAsync(string icao)
        {
            // Get the airport the user is trying to delete.
            // Throws "EntityNotFoundException" if airport does not already exist.
            var airportToDelete = await GetAirportAsync(icao);

            // If the airport exists, remove airport.
            DbContext.Airports.Remove(airportToDelete);

            // Update database.
            await DbContext.SaveChangesAsync();

            // Return deleted airport.
            return airportToDelete;
        }
    }
}
