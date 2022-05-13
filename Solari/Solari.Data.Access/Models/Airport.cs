using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.Data.Access.Models
{
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

        public List<Flight> DepartingFlights { get; set; } = new();

        public List<Flight> ArrivingFlights { get; set; } = new();
    }
}
