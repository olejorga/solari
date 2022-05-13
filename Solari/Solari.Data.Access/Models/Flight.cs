using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solari.Data.Access.Models
{
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
