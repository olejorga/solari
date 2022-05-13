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
    /// A schema for an airline.
    /// 
    /// <list type="bullet">
    /// <item>
    /// <term>Icao</term>
    /// <description>A unique three letter identifier (PK)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Iata</term>
    /// <description>A two letter identifier (not unique)</description>
    /// </item>
    /// 
    /// <item>
    /// <term>Name</term>
    /// <description>The name of the airline</description>
    /// </item>
    /// </list>
    /// </summary>
    [Table("Airlines")]
    public class Airline
    {
        [Key]
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Must be exactly 3 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Icao { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "Must be exactly 2 characters long.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        public string Iata { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Flight> Flights { get; set; } = new();
    }
}
