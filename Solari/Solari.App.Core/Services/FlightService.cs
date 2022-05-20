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
    /// flight endpoints of the REST-API, with extra methods.
    /// </summary>
    public class FlightService : IFlightService
    {
        private readonly HttpClient _HttpClient;

        public FlightService()
        {
            _HttpClient = new HttpClient() { BaseAddress = new Uri(BaseAddress.DataApi) };
        }

        /// <summary>
        /// Gets all flights via the REST-API.
        /// </summary>
        /// <returns>All flights from the REST-API.</returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task<IEnumerable<Flight>> GetFlightsAsync()
        {
            // Request all flights.
            HttpResponseMessage response = await _HttpClient
                .GetAsync("flights");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was successful (200), return the flights.
            if (response.IsSuccessStatusCode)
                return await Json.ToObjectAsync<List<Flight>>(content);

            // For any other status code, throw a exception with
            // the error message from the REST-API.
            else
                throw new Exception(content);
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(string query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets flight by flight number from the REST-API.
        /// </summary>
        /// <param name="flightNumber">The flights three letter ICAO identifier.</param>
        /// <returns>Flight matching the flight number.</returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task<Flight> GetFlightAsync(string flightNumber)
        {
            // Request the flight.
            HttpResponseMessage response = await _HttpClient
                .GetAsync($"flights/{flightNumber}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was successful (200), return the flight.
            if (response.IsSuccessStatusCode)
                return await Json.ToObjectAsync<Flight>(content);

            // For any other status code, throw a exception with
            // the error message from the REST-API.
            else
                throw new Exception(content);
        }

        /// <summary>
        /// Adds an flight via the REST-API.
        /// </summary>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <param name="flight">A flight object.</param>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task AddFlightAsync(Flight flight)
        {
            // Create the flight.
            HttpResponseMessage response = await _HttpClient
                .PostAsJsonAsync("flights", flight);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 201), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.Created)
                throw new Exception(content);
        }

        /// <summary>
        /// Updates an flight via the REST-API.
        /// </summary>
        /// <param name="flight">A updated flight object.</param>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task UpdateFlightAsync(Flight flight)
        {
            // Update the flight.
            HttpResponseMessage response = await _HttpClient
                .PutAsJsonAsync($"flights/{flight.FlightNumber}", flight);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);
        }

        /// <summary>
        /// Delete an flight via the REST-API.
        /// </summary>
        /// <param name="flightNumber">The flights three letter ICAO identifier.</param>
        /// <returns>
        /// Nothing, but throws exception with a helping,
        /// error message if something went wrong.
        /// </returns>
        /// <exception cref="Exception">API error with message.</exception>
        public async Task DeleteFlightAsync(string flightNumber)
        {
            // Delete the flight.
            HttpResponseMessage response = await _HttpClient
                .DeleteAsync($"flights/{flightNumber}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);
        }
    }
}
