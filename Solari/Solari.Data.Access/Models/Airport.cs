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
    [Table("Airports")]
    public class Airport
    {
        /// <summary>
        /// A unique four letter identifier (PK).
        /// </summary>
        [Key]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Must be exactly 4 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Icao { get; set; }

        /// <summary>
        /// A three letter identifier (not unique).
        /// </summary>
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be exactly 3 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
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
