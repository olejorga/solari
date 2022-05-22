using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Solari.Data.Access.Attributes
{
    /// <summary>
    /// A common data annotation validation attribute for IATA, 
    /// ICAO and flight numbers.
    /// 
    /// The only difference is the string min and max length.
    /// </summary>
    public class IdentifierAttribute : ValidationAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }

        public IdentifierAttribute(int MinLength, int MaxLength)
        {
            this.MinLength = MinLength;
            this.MaxLength = MaxLength;

            ErrorMessage = $"" +
                $"Must be between {MinLength} and {MaxLength} characters " +
                $"and only contain uppercase letters (A-Z) and numbers (0-9).";
        }

        public override bool IsValid(object value)
        {
            string icao = value as string;
            Regex pattern = new($"^[A-Z0-9]{{{MinLength},{MaxLength}}}$");

            if (!string.IsNullOrEmpty(icao))
            {
                return pattern.IsMatch(icao);
            }

            return true;
        }
    }
}
