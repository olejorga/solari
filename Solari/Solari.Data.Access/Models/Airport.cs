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
    /// <term>Icao</term>
    /// <description>A unique four letter identifier (PK)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Iata</term>
    /// <description>A three letter identifier (not unique)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Name</term>
    /// <description>The name of the airport</description>
    /// </item>
    /// 
    /// <item>
    /// <term>City</term>
    /// <description>The city of which the airport adheres to</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Departing flights</term>
    /// <description>A list of flights scheduled to depart the airport</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Arriving flights</term>
    /// <description>A list of flights scheduled to arrive at the airport</description>
    /// </item>
    /// </list>
    /// </summary>
    [Table("Airports")]
    public class Airport
    {
        [Key]
        [Required]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Must be exactly 4 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Icao { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be exactly 3 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Iata { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string City { get; set; }

        public virtual List<Flight> DepartingFlights { get; set; } = new();

        public virtual List<Flight> ArrivingFlights { get; set; } = new();
    }
}
