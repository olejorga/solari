using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solari.Data.Access.Repositories;
using Solari.Data.Access.Models;
using Microsoft.AspNetCore.Http;
using Solari.Data.Access.Exceptions;

namespace Solari.Data.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository airlineRepository;

        public AirlineController(IAirlineRepository airlineRepository)
        {
            this.airlineRepository = airlineRepository;
        }

        /// <summary>
        /// Retrieve all airlines.
        /// </summary>
        /// <remarks>Awesomeness!</remarks>
        /// <response code="200">Returns a list of airline objects.</response>
        /// <response code="404">No airlines in database.</response>
        /// <response code="500"></response>
        [HttpGet("airlines")]
        public async Task<ActionResult<IEnumerable<Airline>>> GetAirlines()
        {
            try
            {
                return Ok((await airlineRepository.GetAirlinesAsync()).ToList());
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

        [HttpGet("airlines/{icao}")]
        public async Task<ActionResult<Airline>> GetAirline(string icao)
        {
            try
            {
                return Ok(await airlineRepository.GetAirlineAsync(icao));
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

        [HttpPost("airlines")]
        public async Task<ActionResult<Airline>> CreateAirline(Airline airline)
        {
            try
            {
                // If the user have not provided an airline to be created, return 400.
                if (airline == null)
                    return BadRequest("No airline object provided!");

                var createdAirline = await airlineRepository.AddAirlineAsync(airline);

                // Return a 201 response with the airline object.
                return CreatedAtAction(nameof(GetAirline),
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
                    "Error creating new airline!");
            }
        }

        [HttpPut("airlines/{icao}")]
        public async Task<ActionResult<Airline>> UpdateAirline(string icao, Airline airline)
        {
            try
            {
                // If the user have not provided an airline to update, return 400.
                if (airline == null)
                    return BadRequest("No airline object provided!");

                // If the provided ICAO codes does not match, return 400.
                if (icao.ToUpper() != airline.Icao)
                    return BadRequest("Airline ICAO mismatch!");

                return Ok(await airlineRepository.UpdateAirlineAsync(airline));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating airline!");
            }
        }

        [HttpDelete("airlines/{icao}")]
        public async Task<ActionResult<Airline>> DeleteAirline(string icao)
        {
            try
            {
                return Ok(await airlineRepository.DeleteAirlineAsync(icao));
            }
            catch (EntityNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting airline!");
            }
        }
    }
}
