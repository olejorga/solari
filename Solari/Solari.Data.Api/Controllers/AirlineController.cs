using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solari.Data.Access.Contracts.Repositories;
using Solari.Data.Access.Exceptions;
using Solari.Data.Access.Models;
using Solari.Data.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solari.Data.Api.Controllers
{
    /// <summary>
    /// Restful airlines web service, built with CRUD in mind.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository _airlineRepository;

        public AirlineController(IAirlineRepository airlineRepository)
        {
            _airlineRepository = airlineRepository;
        }

        /// <summary>
        /// Retrieve all airlines.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns a list of airline objects.</response>
        /// <response code="404">No airlines in database.</response>
        /// <response code="500">Error reading from database.</response>
        [HttpGet("airlines")]
        public async Task<ActionResult<IEnumerable<Airline>>> GetAirlines()
        {
            try
            {
                return Ok((await _airlineRepository.GetAirlinesAsync()).ToList());
            }
            catch (EntitiesNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving airlines from the database!");
            }
        }

        /// <summary>
        /// Retrieve airline by ICAO code.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns an airline matching the ICAO code.</response>
        /// <response code="404">No matching airline in database.</response>
        /// <response code="500">Error reading from database.</response>
        [HttpGet("airlines/{icao}")]
        public async Task<ActionResult<Airline>> GetAirline(string icao)
        {
            try
            {
                return Ok(await _airlineRepository.GetAirlineAsync(icao));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving airlines from the database!");
            }
        }

        /// <summary>
        /// Add an airline.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns airline entry in database.</response>
        /// <response code="400">No airline object provided by the user.</response>
        /// <response code="409">Airline already exists.</response>
        /// <response code="500">Error updating database.</response>
        [HttpPost("airlines")]
        public async Task<ActionResult<Airline>> CreateAirline(Airline airline)
        {
            // If mode is invalid, return a custom error.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateError.Create(ModelState));
            }

            try
            {
                // If the user have not provided an airline to be created, return 400.
                if (airline == null)
                {
                    return BadRequest("No airline object provided!");
                }

                Airline createdAirline = await _airlineRepository.AddAirlineAsync(airline);

                // Return a 201 response with the airline object.
                return CreatedAtAction(nameof(CreateAirline),
                    new { createdAirline.Icao }, createdAirline);
            }
            catch (EntityAlreadyExistsException exception)
            {
                // Return a 409 stating a conflict, aka the airline already exists.
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
        /// Update an airline.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns the updated airline entry in the database.</response>
        /// <response code="400">No airline object provided or ICAO codes mismatch.</response>
        /// <response code="404">Airline were not found.</response>
        /// <response code="500">Error updating database.</response>
        [HttpPut("airlines/{icao}")]
        public async Task<ActionResult<Airline>> UpdateAirline(string icao, Airline airline)
        {
            // If mode is invalid, return a custom error.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateError.Create(ModelState));
            }

            try
            {
                // If the user have not provided an airline to update, return 400.
                if (airline == null)
                {
                    return BadRequest("No airline object provided!");
                }

                // If the provided ICAO codes does not match, return 400.
                return icao.ToUpper() != airline.Icao
                    ? BadRequest("Airline ICAO mismatch!")
                    : Ok(await _airlineRepository.UpdateAirlineAsync(airline));
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
        /// Update an airline.
        /// </summary>
        /// <remarks>Returns referenced JSON.</remarks>
        /// <response code="200">Returns the deleted airline entry from the database.</response>
        /// <response code="404">Airline were not found.</response>
        /// <response code="500">Error updating database.</response>
        [HttpDelete("airlines/{icao}")]
        public async Task<ActionResult<Airline>> DeleteAirline(string icao)
        {
            try
            {
                return Ok(await _airlineRepository.DeleteAirlineAsync(icao));
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
