namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for flight numbers.
    /// </summary>
    public class FlightNumberAttribute : IdentifierAttribute
    {
        public FlightNumberAttribute() : base(3, 6)
        {
        }
    }
}
