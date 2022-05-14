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
    /// 
    /// <list type="bullet">
    /// <item>
    /// <term>Flight number</term>
    /// <description>A unique identifier between 3 and 6 letters (PK)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Status</term>
    /// <description>A optional status message for the flight</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Departure time</term>
    /// <description>The estimated time of departure (datetime string)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Arrival time</term>
    /// <description>The estimated time of arrival (datetime string)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Departure gate</term>
    /// <description>The identifier of the departure gate</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Baggage belt</term>
    /// <description>The identifier of the baggage belt</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Departure airport</term>
    /// <description>The object representation of the departure airport</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Arrival airport</term>
    /// <description>The object representation of the arrival airport</description>
    /// </item>
    /// </list>
    /// </summary>
    [Table("Flights")]
    public class Flight
    {
        [Key]
        [Required]
        [StringLength(6, MinimumLength = 3, ErrorMessage = "Must be 3 to 6 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string FlightNumber { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string ArrivalTime { get; set; }

        public string DepartureGate { get; set; }

        public string BaggageBelt { get; set; }

        #region Departure airport
        [ForeignKey("DepartureAirportIcao")]
        [InverseProperty("DepartingFlights")]
        public virtual Airport DepartureAirport { get; set; }
        public string DepartureAirportIcao { get; set; }
        #endregion

        #region Arrival airport
        [ForeignKey("ArrivalAirportIcao")]
        [InverseProperty("ArrivingFlights")]
        public virtual Airport ArrivalAirport { get; set; }
        public string ArrivalAirportIcao { get; set; }
        #endregion

        #region Airline
        [ForeignKey("AirlineIcao")]
        [InverseProperty("Flights")]
        public virtual Airline Airline { get; set; }
        public string AirlineIcao { get; set; }
        #endregion
    }
}
