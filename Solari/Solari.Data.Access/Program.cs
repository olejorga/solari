using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Models;
using Solari.Data.Access.Repositories;

namespace Solari.Data.Access
{
    class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            Console.WriteLine("Hello World!");

            SolariContext ctx = new();
            var repo = new SqlAirlineRepository(ctx);

            await repo.AddAirlineAsync(new Airline { Icao = "TST", Iata = "TS", Name = "Test Airways" });
            //await repo.DeleteAirlineAsync("TST");

            var airlines = await repo.GetAirlinesAsync();

            foreach (Airline a in airlines)
            {
                Console.WriteLine(a.Name);
            }
        }
    }
}
