namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airline ICAO.
    /// </summary>
    public class AirlineIcaoAttribute : IdentifierAttribute
    {
        public AirlineIcaoAttribute() : base(3, 3)
        {
        }
    }
}
