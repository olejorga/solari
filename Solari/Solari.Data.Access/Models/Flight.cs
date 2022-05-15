using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [StringLength(6, MinimumLength = 3, ErrorMessage = "Must be 3 to 6 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
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
        public string DepartureAirportIcao { get; set; }
        #endregion

        #region Arrival airport
        [ForeignKey("ArrivalAirportIcao")]
        [InverseProperty("ArrivingFlights")]
        public virtual Airport ArrivalAirport { get; set; }
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
        /// The ICAO code of the arrival airport (FK).
        /// </summary>
        public string AirlineIcao { get; set; }
        #endregion
    }
}
