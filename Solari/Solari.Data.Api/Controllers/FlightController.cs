using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solari.Data.Access.Contracts.Repositories;
using Solari.Data.Access.Exceptions;
using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solari.Data.Api.Controllers
{
    /// <summary>
    /// Restful flights web service, built with CRUD in mind.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _FlightRepository;

        public FlightController(IFlightRepository flightRepository)
        {
            _FlightRepository = flightRepository;
        }

        /// <summary>
        /// Retrieve all flights.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns a list of flight objects.</response>
        /// <response code="404">No flights in database.</response>
        /// <response code="500">Error reading from database.</response>
        [HttpGet("flights")]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
            try
            {
                return Ok((await _FlightRepository.GetFlightsAsync()).ToList());
            }
            catch (EntitiesNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving flights from the database!");
            }
        }

        /// <summary>
        /// Retrieve flight by flight number.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns a flight matching the flight number.</response>
        /// <response code="404">No matching flight in database.</response>
        /// <response code="500">Error reading from database.</response>
        [HttpGet("flights/{flightNumber}")]
        public async Task<ActionResult<Flight>> GetFlight(string flightNumber)
        {
            try
            {
                return Ok(await _FlightRepository.GetFlightAsync(flightNumber));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving flights from the database!");
            }
        }

        /// <summary>
        /// Add a flight.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns flight entry in database.</response>
        /// <response code="400">No flight object provided by the user.</response>
        /// <response code="409">Flight already exists.</response>
        /// <response code="500">Error updating database.</response>
        [HttpPost("flights")]
        public async Task<ActionResult<Flight>> CreateFlight(Flight flight)
        {
            try
            {
                // If the user have not provided a flight to be created, return 400.
                if (flight == null)
                    return BadRequest("No flight object provided!");

                var createdFlight = await _FlightRepository.AddFlightAsync(flight);

                // Return a 201 response with the flight object.
                return CreatedAtAction(nameof(CreateFlight),
                    new { createdFlight.FlightNumber }, createdFlight);
            }
            catch (EntityAlreadyExistsException exception)
            {
                // Return a 409 stating a conflict, aka the flight already exists.
                return StatusCode(StatusCodes.Status409Conflict,
                    exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating database!");
            }
        }

        /// <summary>
        /// Update a flight.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns the updated flight entry in the database.</response>
        /// <response code="400">No flight object provided or flight numbers mismatch.</response>
        /// <response code="404">Flight were not found.</response>
        /// <response code="500">Error updating database.</response>
        [HttpPut("flights/{flightNumber}")]
        public async Task<ActionResult<Flight>> UpdateFlight(string flightNumber, Flight flight)
        {
            try
            {
                // If the user have not provided a flight to update, return 400.
                if (flight == null)
                    return BadRequest("No flight object provided!");

                // If the provided flight numbers does not match, return 400.
                if (flightNumber.ToUpper() != flight.FlightNumber)
                    return BadRequest("Flight number mismatch!");

                return Ok(await _FlightRepository.UpdateFlightAsync(flight));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating database!");
            }
        }

        /// <summary>
        /// Update a flight.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns the deleted flight entry from the database.</response>
        /// <response code="404">Flight were not found.</response>
        /// <response code="500">Error updating database.</response>
        [HttpDelete("flights/{flightNumber}")]
        public async Task<ActionResult<Flight>> DeleteFlight(string flightNumber)
        {
            try
            {
                return Ok(await _FlightRepository.DeleteFlightAsync(flightNumber));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating database!");
            }
        }
    }
}
