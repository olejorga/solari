namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airline IATA.
    /// </summary>
    public class AirlineIataAttribute : IdentifierAttribute
    {
        public AirlineIataAttribute() : base(2, 2)
        {
        }
    }
}
