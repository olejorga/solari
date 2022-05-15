using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Solari.Data.Access.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airlines",
                columns: table => new
                {
                    Icao = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Iata = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airlines", x => x.Icao);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Icao = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Iata = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Icao);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightNumber = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureGate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaggageBelt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureAirportIcao = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    ArrivalAirportIcao = table.Column<string>(type: "nvarchar(4)", nullable: true),
                    AirlineIcao = table.Column<string>(type: "nvarchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightNumber);
                    table.ForeignKey(
                        name: "FK_Flights_Airlines_AirlineIcao",
                        column: x => x.AirlineIcao,
                        principalTable: "Airlines",
                        principalColumn: "Icao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_ArrivalAirportIcao",
                        column: x => x.ArrivalAirportIcao,
                        principalTable: "Airports",
                        principalColumn: "Icao",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_DepartureAirportIcao",
                        column: x => x.DepartureAirportIcao,
                        principalTable: "Airports",
                        principalColumn: "Icao",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Airlines",
                columns: new[] { "Icao", "Iata", "Name" },
                values: new object[,]
                {
                    { "FOX", "FS", "Flyr" },
                    { "SAS", "SK", "Scandinavian Airlines" },
                    { "NAX", "DY", "Norwegian Air Shuttle" },
                    { "RYR", "FR", "Ryanair" }
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "Icao", "City", "Iata", "Name" },
                values: new object[,]
                {
                    { "ENGM", "Oslo", "OSL", "Gardermoen" },
                    { "ENBR", "Bergen", "BGO", "Flesland" },
                    { "EKCH", "Copenhagen", "CPH", "Kastrup" },
                    { "EGSS", "London", "STN", "Standsted" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightNumber", "AirlineIcao", "ArrivalAirportIcao", "ArrivalTime", "BaggageBelt", "DepartureAirportIcao", "DepartureGate", "DepartureTime", "Status" },
                values: new object[,]
                {
                    { "DY250", "NAX", "ENBR", new DateTime(1970, 1, 1, 10, 45, 0, 0, DateTimeKind.Unspecified), "", "ENGM", "A16", new DateTime(1970, 1, 1, 9, 45, 0, 0, DateTimeKind.Unspecified), "New time 10:15" },
                    { "FS172", "FOX", "ENBR", new DateTime(1970, 1, 1, 11, 30, 0, 0, DateTimeKind.Unspecified), "", "ENGM", "A14", new DateTime(1970, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Boarding" },
                    { "SK346", "SAS", "ENBR", new DateTime(1970, 1, 1, 12, 50, 0, 0, DateTimeKind.Unspecified), "", "ENGM", "", new DateTime(1970, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Cancelled" },
                    { "DY345", "NAX", "EKCH", new DateTime(1970, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified), "", "ENGM", "B45", new DateTime(1970, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Gate closed" },
                    { "SK324", "SAS", "EKCH", new DateTime(1970, 1, 1, 10, 45, 0, 0, DateTimeKind.Unspecified), "", "ENGM", "C72", new DateTime(1970, 1, 1, 9, 45, 0, 0, DateTimeKind.Unspecified), "Last call" },
                    { "FS173", "FOX", "EKCH", new DateTime(1970, 1, 1, 11, 25, 0, 0, DateTimeKind.Unspecified), "", "ENGM", "C69", new DateTime(1970, 1, 1, 10, 35, 0, 0, DateTimeKind.Unspecified), "Go to gate" },
                    { "FR28", "RYR", "EGSS", new DateTime(1970, 1, 1, 9, 15, 0, 0, DateTimeKind.Unspecified), "7", "ENGM", "185", new DateTime(1970, 1, 1, 7, 20, 0, 0, DateTimeKind.Unspecified), "Last bag on belt" },
                    { "FR616", "RYR", "EGSS", new DateTime(1970, 1, 1, 9, 45, 0, 0, DateTimeKind.Unspecified), "9", "EKCH", "C64", new DateTime(1970, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Landed 09:46" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_AirlineIcao",
                table: "Flights",
                column: "AirlineIcao");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalAirportIcao",
                table: "Flights",
                column: "ArrivalAirportIcao");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureAirportIcao",
                table: "Flights",
                column: "DepartureAirportIcao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airlines");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
