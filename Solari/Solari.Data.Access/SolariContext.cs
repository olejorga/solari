using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Solari.Data.Access.Models;

namespace Solari.Data.Access
{
    public class SolariContext : DbContext
    {
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<Flight> Flight { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "donau.hiof.no",
                InitialCatalog = "olejorga",
                UserID = "olejorga",
                Password = "v1FHdrd6lI"
            };

            optionsBuilder.UseSqlServer(builder.ConnectionString.ToString());
        }
    }
}
