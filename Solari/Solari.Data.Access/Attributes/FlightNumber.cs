namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for flight numbers.
    /// </summary>
    public class FlightNumber : Identifier
    {
        public FlightNumber() : base(3, 6)
        { }
    }
}
