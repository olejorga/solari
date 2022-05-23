using Solari.App.Core.Constants;
using Solari.App.Core.Contracts.Services;
using Solari.App.Core.Helpers;
using Solari.Data.Access.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Solari.App.Core.Services
{
    /// <summary>
    /// An abstraction of the HTTP operations made towards the
    /// airline endpoints of the REST-API.
    /// </summary>
    public class AirlineService : IAirlineService
    {
        private readonly HttpClient _httpClient;

        public AirlineService()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(BaseAddress.DataApi) };
        }

        /// <summary>
        /// Gets airline by ICAO code from the REST-API.
        /// </summary>
        /// <param name="icao">The airlines three letter ICAO identifier.</param>
        /// <returns>Airline matching the ICAO code.</returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task<Airline> GetAirlineAsync(string icao)
        {
            // Request the airline.
            HttpResponseMessage response = await _httpClient
                .GetAsync($"airlines/{icao}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was successful (200), return the airline.
            // For any other status code, throw a exception with
            // the error message from the REST-API.
            return response.IsSuccessStatusCode
                ? await Json.ToObjectAsync<Airline>(content)
                : throw new Exception(content);
        }

        /// <summary>
        /// Adds an airline via the REST-API.
        /// </summary>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <param name="airline">A airline object.</param>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task AddAirlineAsync(Airline airline)
        {
            // Create the airline.
            HttpResponseMessage response = await _httpClient
                .PostAsJsonAsync("airlines", airline);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 201), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.Created)
            {
                throw new Exception(content);
            }
        }

        /// <summary>
        /// Updates an airline via the REST-API.
        /// </summary>
        /// <param name="airline">A updated airline object.</param>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task UpdateAirlineAsync(Airline airline)
        {
            // Update the airline.
            HttpResponseMessage response = await _httpClient
                .PutAsJsonAsync($"airlines/{airline.Icao}", airline);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(content);
            }
        }

        /// <summary>
        /// Delete an airline via the REST-API.
        /// </summary>
        /// <param name="icao">The airlines three letter ICAO identifier.</param>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task DeleteAirlineAsync(string icao)
        {
            // Delete the airline.
            HttpResponseMessage response = await _httpClient
                .DeleteAsync($"airlines/{icao}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(content);
            }
        }
    }
}
