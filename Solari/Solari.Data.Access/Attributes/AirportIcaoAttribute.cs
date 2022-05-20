namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airport ICAO.
    /// </summary>
    public class AirportIcaoAttribute : IdentifierAttribute
    {
        public AirportIcaoAttribute() : base(4, 4)
        { }
    }
}
