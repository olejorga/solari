using Solari.Data.Access.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solari.Data.Access.Models
{
    /// <summary>
    /// A schema for an airport.
    /// </summary>
    [Table("Flights")]
    public class Flight
    {
        /// <summary>
        /// A unique identifier between 3 and 6 letters (PK).
        /// </summary>
        [Key]
        [Required]
        [FlightNumber]
        public string FlightNumber { get; set; }

        /// <summary>
        /// A optional status message for the flight.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The estimated time of departure.
        /// </summary>
        [Required]
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// The estimated time of arrival.
        /// </summary>
        [Required]
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// The identifier of the departure gate at the departure airport.
        /// </summary>
        public string DepartureGate { get; set; }

        /// <summary>
        /// The identifier of the baggage belt at the arrival airport.
        /// </summary>
        public string BaggageBelt { get; set; }

        #region Departure airport
        /// <summary>
        /// The aggregated object representation of the departure airport.
        /// </summary>
        [ForeignKey("DepartureAirportIcao")]
        [InverseProperty("DepartingFlights")]
        public virtual Airport DepartureAirport { get; set; }

        /// <summary>
        /// The ICAO code of the departure airport (FK).
        /// </summary>
        [AirportIcao]
        public string DepartureAirportIcao { get; set; }
        #endregion

        #region Arrival airport
        [ForeignKey("ArrivalAirportIcao")]
        [InverseProperty("ArrivingFlights")]
        public virtual Airport ArrivalAirport { get; set; }

        /// <summary>
        /// The ICAO code of the arrival airport (FK).
        /// </summary>
        [AirportIcao]
        public string ArrivalAirportIcao { get; set; }
        #endregion

        #region Airline
        /// <summary>
        /// The aggregated object representation of the arrival airport.
        /// </summary>
        [ForeignKey("AirlineIcao")]
        [InverseProperty("Flights")]
        public virtual Airline Airline { get; set; }

        /// <summary>
        /// The ICAO code of the flights airline (FK).
        /// </summary>
        [AirlineIcao]
        public string AirlineIcao { get; set; }
        #endregion
    }
}
