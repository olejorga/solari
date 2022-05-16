namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A validation attribute for airline ICAO.
    /// </summary>
    public class AirlineIcao : Identifier
    {
        public AirlineIcao() : base(3, 3)
        { }
    }
}
