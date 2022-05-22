namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airport IATA.
    /// </summary>
    public class AirportIataAttribute : IdentifierAttribute
    {
        public AirportIataAttribute() : base(3, 3)
        {
        }
    }
}
