using Solari.App.Core.Constants;
using Solari.App.Core.Contracts.Services;
using Solari.App.Core.Helpers;
using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Solari.App.Core.Services
{
    /// <summary>
    /// An abstraction of the HTTP operations made towards the
    /// airport endpoints of the REST-API, with extra methods.
    /// </summary>
    public class AirportService : IAirportService
    {
        private readonly HttpClient _HttpClient;

        public AirportService()
        {
            _HttpClient = new HttpClient() { BaseAddress = new Uri(BaseAddress.DataApi) };
        }

        /// <summary>
        /// Gets all airports via the REST-API.
        /// </summary>
        /// <returns>All airports from the REST-API.</returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task<IEnumerable<Airport>> GetAirportsAsync()
        {
            // Request all airports.
            HttpResponseMessage response = await _HttpClient
                .GetAsync("airports");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was successful (200), return the airports.
            if (response.IsSuccessStatusCode)
                return await Json.ToObjectAsync<List<Airport>>(content);

            // For any other status code, throw a exception with
            // the error message from the REST-API.
            else
                throw new Exception(content);
        }

        public async Task<IEnumerable<Airport>> SearchAirportsAsync(string query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets airport by ICAO code from the REST-API.
        /// </summary>
        /// <param name="icao">The airports three letter ICAO identifier.</param>
        /// <returns>Airport matching the ICAO code.</returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task<Airport> GetAirportAsync(string icao)
        {
            // Request the airport.
            HttpResponseMessage response = await _HttpClient
                .GetAsync($"airports/{icao}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was successful (200), return the airport.
            if (response.IsSuccessStatusCode)
                return await Json.ToObjectAsync<Airport>(content);

            // For any other status code, throw a exception with
            // the error message from the REST-API.
            else
                throw new Exception(content);
        }

        /// <summary>
        /// Adds an airport via the REST-API.
        /// </summary>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <param name="airport">A airport object.</param>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task AddAirportAsync(Airport airport)
        {
            // Create the airport.
            HttpResponseMessage response = await _HttpClient
                .PostAsJsonAsync("airports", airport);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 201), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception(content);
        }

        /// <summary>
        /// Updates an airport via the REST-API.
        /// </summary>
        /// <param name="airport">A updated airport object.</param>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task UpdateAirportAsync(Airport airport)
        {
            airport.DepartingFlights = null;
            airport.ArrivingFlights = null;

            // Update the airport.
            HttpResponseMessage response = await _HttpClient
                .PutAsJsonAsync($"airports/{airport.Icao}", airport);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);
        }

        /// <summary>
        /// Delete an airport via the REST-API.
        /// </summary>
        /// <param name="icao">The airports three letter ICAO identifier.</param>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task DeleteAirportAsync(string icao)
        {
            // Delete the airport.
            HttpResponseMessage response = await _HttpClient
                .DeleteAsync($"airports/{icao}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);
        }
    }
}
