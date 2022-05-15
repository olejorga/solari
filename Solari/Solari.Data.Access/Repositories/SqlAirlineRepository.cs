using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.Data.Access.Repositories
{
    public class SqlAirlineRepository : IAirlineRepository
    {
        private readonly SolariContext DbContext;

        public SqlAirlineRepository(SolariContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public async Task<IEnumerable<Airline>> GetAirlinesAsync()
        {
            return await DbContext.Airlines
                .Include(e => e.Flights)
                .ToListAsync();
        }

        public async Task<Airline> GetAirlineAsync(string icao)
        {
            return await DbContext.Airlines
                .Include(e => e.Flights)
                .FirstOrDefaultAsync(e => e.Icao == icao);
        }

        public async Task<Airline> AddAirlineAsync(Airline airline)
        {
            var result = await DbContext.Airlines.AddAsync(airline);

            await DbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<Airline> UpdateAirlineAsync(Airline airline)
        {
            var result = await DbContext.Airlines
                .Include(e => e.Flights)
                .FirstOrDefaultAsync(e => e.Icao == airline.Icao);

            if (result != null)
            {
                result = airline;

                await DbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Airline> DeleteAirlineAsync(string icao)
        {
            var result = await DbContext.Airlines
                .Include(e => e.Flights)
                .FirstOrDefaultAsync(e => e.Icao == icao);

            if (result != null)
            {
                DbContext.Airlines.Remove(result);

                await DbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
