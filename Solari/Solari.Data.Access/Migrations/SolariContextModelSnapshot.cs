﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solari.Data.Access;

namespace Solari.Data.Access.Migrations
{
    [DbContext(typeof(SolariContext))]
    partial class SolariContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Solari.Data.Access.Models.Airline", b =>
                {
                    b.Property<string>("Icao")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Iata")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Icao");

                    b.ToTable("Airlines");

                    b.HasData(
                        new
                        {
                            Icao = "FOX",
                            Iata = "FS",
                            Name = "Flyr"
                        },
                        new
                        {
                            Icao = "SAS",
                            Iata = "SK",
                            Name = "Scandinavian Airlines"
                        },
                        new
                        {
                            Icao = "NAX",
                            Iata = "DY",
                            Name = "Norwegian Air Shuttle"
                        },
                        new
                        {
                            Icao = "RYR",
                            Iata = "FR",
                            Name = "Ryanair"
                        });
                });

            modelBuilder.Entity("Solari.Data.Access.Models.Airport", b =>
                {
                    b.Property<string>("Icao")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iata")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Icao");

                    b.ToTable("Airports");

                    b.HasData(
                        new
                        {
                            Icao = "ENGM",
                            City = "Oslo",
                            Iata = "OSL",
                            Name = "Gardermoen"
                        },
                        new
                        {
                            Icao = "ENBR",
                            City = "Bergen",
                            Iata = "BGO",
                            Name = "Flesland"
                        },
                        new
                        {
                            Icao = "EKCH",
                            City = "Copenhagen",
                            Iata = "CPH",
                            Name = "Kastrup"
                        },
                        new
                        {
                            Icao = "EGSS",
                            City = "London",
                            Iata = "STN",
                            Name = "Standsted"
                        });
                });

            modelBuilder.Entity("Solari.Data.Access.Models.Flight", b =>
                {
                    b.Property<string>("FlightNumber")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("AirlineIcao")
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("ArrivalAirportIcao")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("ArrivalTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BaggageBelt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureAirportIcao")
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("DepartureGate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FlightNumber");

                    b.HasIndex("AirlineIcao");

                    b.HasIndex("ArrivalAirportIcao");

                    b.HasIndex("DepartureAirportIcao");

                    b.ToTable("Flights");

                    b.HasData(
                        new
                        {
                            FlightNumber = "DY250",
                            AirlineIcao = "NAX",
                            ArrivalAirportIcao = "ENBR",
                            ArrivalTime = "",
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "A16",
                            DepartureTime = "",
                            Status = "New time 10:15"
                        },
                        new
                        {
                            FlightNumber = "DY345",
                            AirlineIcao = "NAX",
                            ArrivalAirportIcao = "EKCH",
                            ArrivalTime = "",
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "B45",
                            DepartureTime = "",
                            Status = "Gate closed"
                        },
                        new
                        {
                            FlightNumber = "SK324",
                            AirlineIcao = "SAS",
                            ArrivalAirportIcao = "EKCH",
                            ArrivalTime = "",
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "C72",
                            DepartureTime = "",
                            Status = "Last call"
                        },
                        new
                        {
                            FlightNumber = "FS172",
                            AirlineIcao = "FOX",
                            ArrivalAirportIcao = "ENBR",
                            ArrivalTime = "",
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "A14",
                            DepartureTime = "",
                            Status = "Boarding"
                        },
                        new
                        {
                            FlightNumber = "FS173",
                            AirlineIcao = "FOX",
                            ArrivalAirportIcao = "EKCH",
                            ArrivalTime = "",
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "C69",
                            DepartureTime = "",
                            Status = "Go to gate"
                        },
                        new
                        {
                            FlightNumber = "FR28",
                            AirlineIcao = "RYR",
                            ArrivalAirportIcao = "EGSS",
                            ArrivalTime = "",
                            BaggageBelt = "7",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "185",
                            DepartureTime = "",
                            Status = "Last bag on belt"
                        },
                        new
                        {
                            FlightNumber = "SK346",
                            AirlineIcao = "SAS",
                            ArrivalAirportIcao = "ENBR",
                            ArrivalTime = "",
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "",
                            DepartureTime = "",
                            Status = "Cancelled"
                        },
                        new
                        {
                            FlightNumber = "FR616",
                            AirlineIcao = "RYR",
                            ArrivalAirportIcao = "EGSS",
                            ArrivalTime = "",
                            BaggageBelt = "9",
                            DepartureAirportIcao = "EKCH",
                            DepartureGate = "C64",
                            DepartureTime = "",
                            Status = "Landed 09:46"
                        });
                });

            modelBuilder.Entity("Solari.Data.Access.Models.Flight", b =>
                {
                    b.HasOne("Solari.Data.Access.Models.Airline", "Airline")
                        .WithMany("Flights")
                        .HasForeignKey("AirlineIcao");

                    b.HasOne("Solari.Data.Access.Models.Airport", "ArrivalAirport")
                        .WithMany("ArrivingFlights")
                        .HasForeignKey("ArrivalAirportIcao");

                    b.HasOne("Solari.Data.Access.Models.Airport", "DepartureAirport")
                        .WithMany("DepartingFlights")
                        .HasForeignKey("DepartureAirportIcao");

                    b.Navigation("Airline");

                    b.Navigation("ArrivalAirport");

                    b.Navigation("DepartureAirport");
                });

            modelBuilder.Entity("Solari.Data.Access.Models.Airline", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("Solari.Data.Access.Models.Airport", b =>
                {
                    b.Navigation("ArrivingFlights");

                    b.Navigation("DepartingFlights");
                });
#pragma warning restore 612, 618
        }
    }
}
