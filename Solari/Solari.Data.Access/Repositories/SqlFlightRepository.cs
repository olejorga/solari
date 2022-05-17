using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Contracts.Repositories;
using Solari.Data.Access.Exceptions;
using Solari.Data.Access.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Solari.Data.Access.Repositories
{
    /// <summary>
    /// An abstraction of the flights SQL table, 
    /// built with EF Core 5 and designed with CRUD in mind.
    /// </summary>
    public class SqlFlightRepository : IFlightRepository
    {
        private readonly SolariContext _DbContext;

        public SqlFlightRepository(SolariContext dbContext)
        {
            _DbContext = dbContext;
        }

        /// <summary>
        /// Gets all flights in repository.
        /// </summary>
        /// <returns>All flights in repository.</returns>
        /// <exception cref="EntitiesNotFoundException">No flights were found.</exception>
        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            // Get all flights.
            var flights = await _DbContext.Flights
                .Include(e => e.Airline)
                .Include(e => e.DepartureAirport)
                .Include(e => e.ArrivalAirport)
                .ToListAsync();

            // If no flights were found, throw exception.
            if (flights.Count == 0)
                throw new EntitiesNotFoundException("No flights were found!");

            // If flights were found.
            return flights;
        }

        /// <summary>
        /// Gets flight by flight number.
        /// </summary>
        /// <param name="flightNumber">The flights flight number.</param>
        /// <returns>Flight matching the flight number.</returns>
        /// <exception cref="EntityNotFoundException">No flight were found.</exception>
        public async Task<Flight> GetFlightAsync(string flightNumber)
        {
            // Enforcing that the flight number is uppercase.
            flightNumber = flightNumber.ToUpper();

            // Get flight by flight number.
            var flight = await _DbContext.Flights
                .Include(e => e.Airline)
                .Include(e => e.DepartureAirport)
                .Include(e => e.ArrivalAirport)
                .FirstOrDefaultAsync(e => e.FlightNumber == flightNumber.ToUpper());

            // If flight is not found, throw exception.
            if (flight == null)
                throw new EntityNotFoundException($"Flight with ICAO = {flightNumber} not found!");

            // If flight is found, return flight.
            return flight;
        }

        /// <summary>
        /// Adds a flight to the repository.
        /// </summary>
        /// <param name="flight">A flight object.</param>
        /// <returns>The added flight entry in the repository.</returns>
        /// <exception cref="EntityAlreadyExistsException">Flight already exists.</exception>
        public async Task<Flight> AddFlightAsync(Flight flight)
        {
            try
            {
                // Check if a flight with the same flight number exists.
                // Throws "EntityNotFoundException" if flight does not already exist.
                await GetFlightAsync(flight.FlightNumber);

                // If the flight does already exist, throw exception.
                throw new EntityAlreadyExistsException("Flight already exists!");
            }
            catch (EntityNotFoundException)
            {
                // If the flight does not exist, create flight.
                await _DbContext.Flights.AddAsync(flight);

                // Update database.
                await _DbContext.SaveChangesAsync();

                // Get and return the created flight.
                return await GetFlightAsync(flight.FlightNumber);
            }
        }

        /// <summary>
        /// Updates a flight in the repository.
        /// </summary>
        /// <param name="flight">A updated flight object.</param>
        /// <returns>The updated flight entry in the repository.</returns>
        /// <exception cref="EntityNotFoundException">No flight were found.</exception>
        public async Task<Flight> UpdateFlightAsync(Flight flight)
        {
            // Get the flight the user is trying to update exists.
            // Throws "EntityNotFoundException" if flight does not already exist.
            var flightToUpdate = await GetFlightAsync(flight.FlightNumber);

            // If the flight exists, update flight.
            flightToUpdate.FlightNumber = flight.FlightNumber;
            flightToUpdate.Status = flight.Status;
            flightToUpdate.DepartureTime = flight.DepartureTime;
            flightToUpdate.ArrivalTime = flight.ArrivalTime;
            flightToUpdate.DepartureGate = flight.DepartureGate;
            flightToUpdate.BaggageBelt = flight.BaggageBelt;
            flightToUpdate.DepartureAirportIcao = flight.DepartureAirportIcao;
            flightToUpdate.ArrivalAirportIcao = flight.ArrivalAirportIcao;

            // Update database.
            await _DbContext.SaveChangesAsync();

            // Return the updated flight.
            return flightToUpdate;
        }

        /// <summary>
        /// Delete a flight from the repository.
        /// </summary>
        /// <param name="flightNumber">The flights flight number.</param>
        /// <returns>The deleted flight entry in the repository.</returns>
        /// <exception cref="EntityNotFoundException">No flight were found.</exception>
        public async Task<Flight> DeleteFlightAsync(string flightNumber)
        {
            // Get the flight the user is trying to delete.
            // Throws "EntityNotFoundException" if flight does not already exist.
            var flightToDelete = await GetFlightAsync(flightNumber);

            // If the flight exists, remove flight.
            _DbContext.Flights.Remove(flightToDelete);

            // Update database.
            await _DbContext.SaveChangesAsync();

            // Return deleted flight.
            return flightToDelete;
        }
    }
}
