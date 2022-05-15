using Solari.Data.Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solari.Data.Api.RequestModels
{
    public class AirlineRequestModel : Airline
    {
        [Obsolete("This property has been disabled", true)]
        public override List<Flight> Flights { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
