using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Solari.Data.Access.Attributes
{
    public class Identifier : ValidationAttribute
    {
        public int Length { get; set; }

        public Identifier(int Length)
        {
            this.Length = Length;
            ErrorMessage = $"Must be exactly {Length} long and only contain uppercase letters (A-Z) and numbers (0-9).";
        }

        public override bool IsValid(object value)
        {
            string icaoCode = value as string;
            Regex icaoPattern = new($"^[A-Z0-9]{{{Length}}}$");

            if (!string.IsNullOrEmpty(icaoCode))
                return icaoPattern.IsMatch(icaoCode);

            return true;
        }
    }
}
