namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airport ICAO.
    /// </summary>
    public class AirportIcao : Identifier
    {
        public AirportIcao() : base(4, 4)
        { }
    }
}
