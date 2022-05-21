using Solari.Data.Access.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Solari.Data.Access.Models
{
    /// <summary>
    /// A schema for an airline.
    /// </summary>
    [Table("Airlines")]
    public class Airline
    {
        /// <summary>
        /// A unique three letter identifier (PK).
        /// </summary>
        [Key]
        [Required]
        [AirlineIcao]
        public string Icao { get; set; }

        /// <summary>
        /// A two letter identifier (not unique).
        /// </summary>
        [Required]
        [AirlineIata]
        public string Iata { get; set; }

        /// <summary>
        /// The name of the airline
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// A aggregated list of flights flying for the airline.
        /// </summary>
        public virtual List<Flight> Flights { get; set; } = new();
    }
}
