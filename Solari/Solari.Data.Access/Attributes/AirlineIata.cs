namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airline IATA.
    /// </summary>
    public class AirlineIata : Identifier
    {
        public AirlineIata() : base(2, 2)
        { }
    }
}
