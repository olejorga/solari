using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Solari.Data.Access.Repositories;
using Solari.Data.Access.Models;
using Microsoft.AspNetCore.Http;

namespace Solari.Data.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirlineController : ControllerBase
    {
        private readonly IAirlineRepository airlineRepository;

        public AirlineController(IAirlineRepository airlineRepository)
        {
            this.airlineRepository = airlineRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Airline>>> GetAirlines()
        {
            try
            {
                return Ok((await airlineRepository.GetAirlinesAsync()).ToList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving airlines from the database!");
            }
        }

        [HttpGet("{icao}")]
        public async Task<ActionResult<Airline>> GetAirline(string icao)
        {
            try
            {
                var result = await airlineRepository.GetAirlineAsync(icao);

                if (result == null) return NotFound();

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving airlines from the database!");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Airline>> CreateAirline(Airline airline)
        {
            try
            {
                if (airline == null)
                    return BadRequest("No airline object provided!");

                var existingAirline = await airlineRepository.GetAirlineAsync(airline.Icao);

                if (existingAirline != null)
                    return StatusCode(StatusCodes.Status303SeeOther, existingAirline);

                var createdAirline = await airlineRepository.AddAirlineAsync(airline);

                return CreatedAtAction(nameof(GetAirline),
                    new { createdAirline.Icao }, createdAirline);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new airline!");
            }
        }

        [HttpPut("{icao}")]
        public async Task<ActionResult<Airline>> UpdateAirline(string icao, Airline airline)
        {
            try
            {
                if (icao != airline.Icao)
                    return BadRequest("Airline ICAO mismatch!");

                var airlineToUpdate = await airlineRepository.GetAirlineAsync(icao);

                if (airlineToUpdate == null)
                    return NotFound($"Airline with ICAO = {icao} not found!");

                return Ok(await airlineRepository.UpdateAirlineAsync(airline));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating airline!");
            }
        }

        [HttpDelete("{icao}")]
        public async Task<ActionResult<Airline>> DeleteAirline(string icao)
        {
            try
            {
                var airlineToUpdate = await airlineRepository.GetAirlineAsync(icao);

                if (airlineToUpdate == null)
                    return NotFound($"Airline with ICAO = {icao} not found!");

                return Ok(await airlineRepository.DeleteAirlineAsync(icao));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting airline!");
            }
        }
    }
}
