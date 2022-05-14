using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed some sample data for demonstration purposes
            new AirlineEntityTypeConfiguration().Configure(modelBuilder.Entity<Airline>());
            new AirportEntityTypeConfiguration().Configure(modelBuilder.Entity<Airport>());
            new FlightEntityTypeConfiguration().Configure(modelBuilder.Entity<Flight>());
        }

        public class AirlineEntityTypeConfiguration : IEntityTypeConfiguration<Airline>
        {
            public void Configure(EntityTypeBuilder<Airline> builder)
            {
                #region Seeding airlines
                builder.HasData(new Airline { Icao = "FOX", Iata = "FS", Name = "Flyr" });
                builder.HasData(new Airline { Icao = "SAS", Iata = "SK", Name = "Scandinavian Airlines" });
                builder.HasData(new Airline { Icao = "NAX", Iata = "DY", Name = "Norwegian Air Shuttle" });
                builder.HasData(new Airline { Icao = "RYR", Iata = "FR", Name = "Ryanair" });
                #endregion
            }
        }

        public class AirportEntityTypeConfiguration : IEntityTypeConfiguration<Airport>
        {
            public void Configure(EntityTypeBuilder<Airport> builder)
            {
                #region Seeding airports
                builder.HasData(new Airport { Icao = "ENGM", Iata = "OSL", Name = "Gardermoen", City = "Oslo" });
                builder.HasData(new Airport { Icao = "ENBR", Iata = "BGO", Name = "Flesland", City = "Bergen" });
                builder.HasData(new Airport { Icao = "EKCH", Iata = "CPH", Name = "Kastrup", City = "Copenhagen" });
                builder.HasData(new Airport { Icao = "EGSS", Iata = "STN", Name = "Standsted", City = "London" });
                #endregion
            }
        }

        public class FlightEntityTypeConfiguration : IEntityTypeConfiguration<Flight>
        {
            public void Configure(EntityTypeBuilder<Flight> builder)
            {
                #region Seeding flights
                builder.HasData(new Flight { FlightNumber = "DY250", AirlineIcao = "NAX", Status = "New time 10:15", DepartureTime = "", ArrivalTime = "", DepartureGate = "A16", BaggageBelt = "", DepartureAirportIcao = "ENGM", ArrivalAirportIcao = "ENBR" });
                builder.HasData(new Flight { FlightNumber = "DY345", AirlineIcao = "NAX", Status = "Gate closed", DepartureTime = "", ArrivalTime = "", DepartureGate = "B45", BaggageBelt = "", DepartureAirportIcao = "ENGM", ArrivalAirportIcao = "EKCH" });
                builder.HasData(new Flight { FlightNumber = "SK324", AirlineIcao = "SAS", Status = "Last call", DepartureTime = "", ArrivalTime = "", DepartureGate = "C72", BaggageBelt = "", DepartureAirportIcao = "ENGM", ArrivalAirportIcao = "EKCH" });
                builder.HasData(new Flight { FlightNumber = "FS172", AirlineIcao = "FOX", Status = "Boarding", DepartureTime = "", ArrivalTime = "", DepartureGate = "A14", BaggageBelt = "", DepartureAirportIcao = "ENGM", ArrivalAirportIcao = "ENBR" });
                builder.HasData(new Flight { FlightNumber = "FS173", AirlineIcao = "FOX", Status = "Go to gate", DepartureTime = "", ArrivalTime = "", DepartureGate = "C69", BaggageBelt = "", DepartureAirportIcao = "ENGM", ArrivalAirportIcao = "EKCH" });
                builder.HasData(new Flight { FlightNumber = "FR28", AirlineIcao = "RYR", Status = "Last bag on belt", DepartureTime = "", ArrivalTime = "", DepartureGate = "185", BaggageBelt = "7", DepartureAirportIcao = "ENGM", ArrivalAirportIcao = "EGSS" });
                builder.HasData(new Flight { FlightNumber = "SK346", AirlineIcao = "SAS", Status = "Cancelled", DepartureTime = "", ArrivalTime = "", DepartureGate = "", BaggageBelt = "", DepartureAirportIcao = "ENGM", ArrivalAirportIcao = "ENBR" });
                builder.HasData(new Flight { FlightNumber = "FR616", AirlineIcao = "RYR", Status = "Landed 09:46", DepartureTime = "", ArrivalTime = "", DepartureGate = "C64", BaggageBelt = "9", DepartureAirportIcao = "EKCH", ArrivalAirportIcao = "EGSS" });
                #endregion
            }
        }
    }
}
