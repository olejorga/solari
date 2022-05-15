using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be exactly 3 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Icao { get; set; }

        /// <summary>
        /// A two letter identifier (not unique).
        /// </summary>
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Must be exactly 2 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
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
