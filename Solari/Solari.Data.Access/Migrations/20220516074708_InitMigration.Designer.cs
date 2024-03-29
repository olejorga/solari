﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Solari.Data.Access;

namespace Solari.Data.Access.Migrations
{
    [DbContext(typeof(SolariContext))]
    [Migration("20220516074708_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Solari.Data.Access.Models.Airline", b =>
                {
                    b.Property<string>("Icao")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Iata")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iata")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AirlineIcao")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ArrivalAirportIcao")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("BaggageBelt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureAirportIcao")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartureGate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
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
                            ArrivalTime = new DateTime(1970, 1, 1, 10, 45, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "A16",
                            DepartureTime = new DateTime(1970, 1, 1, 9, 45, 0, 0, DateTimeKind.Unspecified),
                            Status = "New time 10:15"
                        },
                        new
                        {
                            FlightNumber = "DY345",
                            AirlineIcao = "NAX",
                            ArrivalAirportIcao = "EKCH",
                            ArrivalTime = new DateTime(1970, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "B45",
                            DepartureTime = new DateTime(1970, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Gate closed"
                        },
                        new
                        {
                            FlightNumber = "SK324",
                            AirlineIcao = "SAS",
                            ArrivalAirportIcao = "EKCH",
                            ArrivalTime = new DateTime(1970, 1, 1, 10, 45, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "C72",
                            DepartureTime = new DateTime(1970, 1, 1, 9, 45, 0, 0, DateTimeKind.Unspecified),
                            Status = "Last call"
                        },
                        new
                        {
                            FlightNumber = "FS172",
                            AirlineIcao = "FOX",
                            ArrivalAirportIcao = "ENBR",
                            ArrivalTime = new DateTime(1970, 1, 1, 11, 30, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "A14",
                            DepartureTime = new DateTime(1970, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                            Status = "Boarding"
                        },
                        new
                        {
                            FlightNumber = "FS173",
                            AirlineIcao = "FOX",
                            ArrivalAirportIcao = "EKCH",
                            ArrivalTime = new DateTime(1970, 1, 1, 11, 25, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "C69",
                            DepartureTime = new DateTime(1970, 1, 1, 10, 35, 0, 0, DateTimeKind.Unspecified),
                            Status = "Go to gate"
                        },
                        new
                        {
                            FlightNumber = "FR28",
                            AirlineIcao = "RYR",
                            ArrivalAirportIcao = "EGSS",
                            ArrivalTime = new DateTime(1970, 1, 1, 9, 15, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "7",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "185",
                            DepartureTime = new DateTime(1970, 1, 1, 7, 20, 0, 0, DateTimeKind.Unspecified),
                            Status = "Last bag on belt"
                        },
                        new
                        {
                            FlightNumber = "SK346",
                            AirlineIcao = "SAS",
                            ArrivalAirportIcao = "ENBR",
                            ArrivalTime = new DateTime(1970, 1, 1, 12, 50, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "",
                            DepartureAirportIcao = "ENGM",
                            DepartureGate = "",
                            DepartureTime = new DateTime(1970, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = "Cancelled"
                        },
                        new
                        {
                            FlightNumber = "FR616",
                            AirlineIcao = "RYR",
                            ArrivalAirportIcao = "EGSS",
                            ArrivalTime = new DateTime(1970, 1, 1, 9, 45, 0, 0, DateTimeKind.Unspecified),
                            BaggageBelt = "9",
                            DepartureAirportIcao = "EKCH",
                            DepartureGate = "C64",
                            DepartureTime = new DateTime(1970, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified),
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
