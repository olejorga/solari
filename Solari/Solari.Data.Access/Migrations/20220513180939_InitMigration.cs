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
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureGate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArrivalGate = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
