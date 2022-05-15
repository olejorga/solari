using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solari.Data.Access.Exceptions;
using Solari.Data.Access.Models;
using Solari.Data.Access.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solari.Data.Api.Controllers
{
    /// <summary>
    /// Restful airports web service, built with CRUD in mind.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportRepository airportRepository;

        public AirportController(IAirportRepository airportRepository)
        {
            this.airportRepository = airportRepository;
        }

        /// <summary>
        /// Retrieve all airports.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns a list of airport objects.</response>
        /// <response code="404">No airports in database.</response>
        /// <response code="500">Error reading from database.</response>
        [HttpGet("airports")]
        public async Task<ActionResult<IEnumerable<Airport>>> GetAirports()
        {
            try
            {
                return Ok((await airportRepository.GetAirportsAsync()).ToList());
            }
            catch (EntitiesNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving airports from the database!");
            }
        }

        /// <summary>
        /// Retrieve airport by ICAO code.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns an airport matching the ICAO code.</response>
        /// <response code="404">No matching airport in database.</response>
        /// <response code="500">Error reading from database.</response>
        [HttpGet("airports/{icao}")]
        public async Task<ActionResult<Airport>> GetAirport(string icao)
        {
            try
            {
                return Ok(await airportRepository.GetAirportAsync(icao));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving airports from the database!");
            }
        }

        /// <summary>
        /// Add an airport.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns airport entry in database.</response>
        /// <response code="400">No airport object provided by the user.</response>
        /// <response code="409">Airport already exists.</response>
        /// <response code="500">Error updating database.</response>
        [HttpPost("airports")]
        public async Task<ActionResult<Airport>> CreateAirport(Airport airport)
        {
            try
            {
                // If the user have not provided an airport to be created, return 400.
                if (airport == null)
                    return BadRequest("No airport object provided!");

                var createdAirport = await airportRepository.AddAirportAsync(airport);

                // Return a 201 response with the airport object.
                return CreatedAtAction(nameof(CreateAirport),
                    new { createdAirport.Icao }, createdAirport);
            }
            catch (EntityAlreadyExistsException exception)
            {
                // Return a 409 stating a conflict, aka the airport already exists.
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
        /// Update an airport.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns the updated airport entry in the database.</response>
        /// <response code="400">No airport object provided or ICAO codes mismatch.</response>
        /// <response code="404">Airport were not found.</response>
        /// <response code="500">Error updating database.</response>
        [HttpPut("airports/{icao}")]
        public async Task<ActionResult<Airport>> UpdateAirport(string icao, Airport airport)
        {
            try
            {
                // If the user have not provided an airport to update, return 400.
                if (airport == null)
                    return BadRequest("No airport object provided!");

                // If the provided ICAO codes does not match, return 400.
                if (icao.ToUpper() != airport.Icao)
                    return BadRequest("Airport ICAO mismatch!");

                return Ok(await airportRepository.UpdateAirportAsync(airport));
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
        /// Update an airport.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns the deleted airport entry from the database.</response>
        /// <response code="404">Airport were not found.</response>
        /// <response code="500">Error updating database.</response>
        [HttpDelete("airports/{icao}")]
        public async Task<ActionResult<Airport>> DeleteAirport(string icao)
        {
            try
            {
                return Ok(await airportRepository.DeleteAirportAsync(icao));
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
