using Solari.App.Core.Constants;
using Solari.App.Core.Contracts.Services;
using Solari.App.Core.Helpers;
using Solari.Data.Access.Exceptions;
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
        private readonly HttpClient _HttpClient;

        public AirlineService()
        {
            _HttpClient = new HttpClient() { BaseAddress = new Uri(BaseAddress.DataApi) };
        }

        public async Task<Airline> GetAirlineAsync(string icao)
        {
            // Request the airline.
            HttpResponseMessage response = await _HttpClient
                .GetAsync($"airlines/{icao}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was successful (200), return the airline.
            if (response.IsSuccessStatusCode)
                return await Json.ToObjectAsync<Airline>(content);

            // For any other status code, throw a exception with
            // the error message from the REST-API.
            else
                throw new Exception(content);
        }

        public async void AddAirlineAsync(Airline airline)
        {
            // Create the airline.
            HttpResponseMessage response = await _HttpClient
                .PostAsJsonAsync("airlines", airline);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);
        }

        public async void UpdateAirlineAsync(Airline airline)
        {
            // Update the airline.
            HttpResponseMessage response = await _HttpClient
                .PutAsJsonAsync($"airlines/{airline.Icao}", airline);

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);
        }

        public async void DeleteAirlineAsync(string icao)
        {
            // Delete the airline.
            HttpResponseMessage response = await _HttpClient
                .DeleteAsync($"airlines/{icao}");

            // Read the contents of the body of the response.
            string content = await response.Content.ReadAsStringAsync();

            // If the request was unsuccessful (Not 200), throw a
            // exception with the error message from the REST-API.
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception(content);
        }
    }
}
