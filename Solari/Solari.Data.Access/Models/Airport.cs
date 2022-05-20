using Solari.Data.Access.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solari.Data.Access.Models
{
    /// <summary>
    /// A schema for an airport.
    /// </summary>
    [Table("Airports")]
    public class Airport
    {
        /// <summary>
        /// A unique four letter identifier (PK).
        /// </summary>
        [Key]
        [Required]
        [AirportIcaoAttribute]
        public string Icao { get; set; }

        /// <summary>
        /// A three letter identifier (not unique).
        /// </summary>
        [Required]
        [AirportIataAttribute]
        public string Iata { get; set; }

        /// <summary>
        /// The name of the airport.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The city of which the airport adheres to.
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// A aggregated list of flights scheduled to depart the airport.
        /// </summary>
        public virtual List<Flight> DepartingFlights { get; set; } = new();

        /// <summary>
        /// A aggregated list of flights scheduled to arrive the airport.
        /// </summary>
        public virtual List<Flight> ArrivingFlights { get; set; } = new();
    }
}
