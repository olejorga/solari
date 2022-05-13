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
    /// <term>Arrival gate</term>
    /// <description>The identifier of the arrival gate</description>
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

        [Required]
        public string DepartureGate { get; set; }

        [Required]
        public string ArrivalGate { get; set; }

        [ForeignKey("DepartureAirportIcao")]
        [InverseProperty("DepartingFlights")]
        public virtual Airport DepartureAirport { get; set; }

        [ForeignKey("ArrivalAirportIcao")]
        [InverseProperty("ArrivingFlights")]
        public virtual Airport ArrivalAirport { get; set; }

        [ForeignKey("AirlineIcao")]
        [InverseProperty("Flights")]
        public Airline Airline { get; set; }
    }
}
