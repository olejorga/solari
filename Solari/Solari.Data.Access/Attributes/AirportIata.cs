namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airport IATA.
    /// </summary>
    public class AirportIata : Identifier
    {
        public AirportIata() : base(3, 3)
        { }
    }
}
