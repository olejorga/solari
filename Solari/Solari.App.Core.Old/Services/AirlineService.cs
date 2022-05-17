using Solari.App.Core.Constants;
using Solari.App.Contracts.Services;
using Solari.App.Core.Contracts.Services;
using Solari.App.Helpers;
using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solari.App.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly HttpClient _HttpClient;

        public AirlineService()
        {
            _HttpClient = new HttpClient() { BaseAddress = new Uri(BaseAddress.DataApi) };
        }

        public Task<bool> AddAirlineAsync(Airline airline)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAirlineAsync(string icao)
        {
            throw new NotImplementedException();
        }

        public async Task<Airline> GetAirlineAsync(string icao)
        {
            Airline airline = null;
            HttpResponseMessage response = await _HttpClient.GetAsync($"airlines/{icao}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                airline = await Json.ToObjectAsync<Airline>(content);
            }

            return airline;
        }

        public Task<bool> UpdateAirlineAsync(Airline airline)
        {
            throw new NotImplementedException();
        }
    }
}
