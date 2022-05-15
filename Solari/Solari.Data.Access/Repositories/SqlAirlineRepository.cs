using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Exceptions;
using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.Data.Access.Repositories
{
    /// <summary>
    /// An abstraction of the airlines SQL table, 
    /// built with EF Core 5 and designed with CRUD in mind.
    /// </summary>
    public class SqlAirlineRepository : IAirlineRepository
    {
        private readonly SolariContext DbContext;

        public SqlAirlineRepository(SolariContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Gets all airlines in repository.
        /// </summary>
        /// <returns>All airlines in repository.</returns>
        /// <exception cref="EntitiesNotFoundException">No airlines were found.</exception>
        public async Task<IEnumerable<Airline>> GetAirlinesAsync()
        {
            // Get all airlines.
            var airlines = await DbContext.Airlines
                .Include(e => e.Flights)
                .ToListAsync();

            // If no airlines were found, throw exception.
            if (airlines.Count == 0)
                throw new EntitiesNotFoundException("No airlines were found!");

            // If airlines were found.
            return airlines;
        }

        /// <summary>
        /// Gets airline by ICAO code.
        /// </summary>
        /// <param name="icao">The airlines three letter ICAO identifier.</param>
        /// <returns>Airline matching the ICAO code.</returns>
        /// <exception cref="EntityNotFoundException">No airline were found.</exception>
        public async Task<Airline> GetAirlineAsync(string icao)
        {
            // Enforcing that the ICAO code is uppercase.
            icao = icao.ToUpper();

            // Get airline by ICAO code.
            var airline = await DbContext.Airlines
                .Include(e => e.Flights)
                .FirstOrDefaultAsync(e => e.Icao == icao.ToUpper());

            // If airline is not found, throw exception.
            if (airline == null)
                throw new EntityNotFoundException($"Airline with ICAO = {icao} not found!");

            // If airline is found, return airline.
            return airline;
        }

        /// <summary>
        /// Adds an airline to the repository.
        /// </summary>
        /// <param name="airline">A airline object.</param>
        /// <returns>The added airline entry in the repository.</returns>
        /// <exception cref="EntityAlreadyExistsException">Airline already exists.</exception>
        public async Task<Airline> AddAirlineAsync(Airline airline)
        {
            try
            {
                // Check if an airline with the same ICAO code exists.
                // Throws "EntityNotFoundException" if airline does not already exist.
                await GetAirlineAsync(airline.Icao);

                // If the airline does already exist, throw exception.
                throw new EntityAlreadyExistsException("Airline already exists!");
            }
            catch (EntityNotFoundException)
            {
                // If the airline does not exist, create airline.
                await DbContext.Airlines.AddAsync(airline);

                // Update database.
                await DbContext.SaveChangesAsync();

                // Get and return the created airline.
                return await GetAirlineAsync(airline.Icao);
            }
        }

        /// <summary>
        /// Updates an airline in the repository.
        /// </summary>
        /// <param name="airline">A updated airline object.</param>
        /// <returns>The updated airline entry in the repository.</returns>
        /// <exception cref="EntityNotFoundException">No airline were found.</exception>
        public async Task<Airline> UpdateAirlineAsync(Airline airline)
        {
            // Get the airline the user is trying to update exists.
            // Throws "EntityNotFoundException" if airline does not already exist.
            var airlineToUpdate = await GetAirlineAsync(airline.Icao);

            // If the airline exists, update airline.
            airlineToUpdate.Icao = airline.Icao;
            airlineToUpdate.Iata = airline.Iata;
            airlineToUpdate.Name = airline.Name;

            // Update database.
            await DbContext.SaveChangesAsync();

            // Return the updated airline.
            return airlineToUpdate;
        }

        /// <summary>
        /// Delete an airline from the repository.
        /// </summary>
        /// <param name="icao">The airlines three letter ICAO identifier.</param>
        /// <returns>The deleted airline entry in the repository.</returns>
        /// <exception cref="EntityNotFoundException">No airline were found.</exception>
        public async Task<Airline> DeleteAirlineAsync(string icao)
        {
            // Get the airline the user is trying to delete.
            // Throws "EntityNotFoundException" if airline does not already exist.
            var airlineToDelete = await GetAirlineAsync(icao);

            // If the airline exists, remove airline.
            DbContext.Airlines.Remove(airlineToDelete);

            // Update database.
            await DbContext.SaveChangesAsync();

            // Return deleted airline.
            return airlineToDelete;
        }
    }
}
