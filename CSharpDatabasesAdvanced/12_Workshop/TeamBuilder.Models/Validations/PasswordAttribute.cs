using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TeamBuilder.Models.Validations
{
   public class PasswordAttribute:ValidationAttribute
    {
        private int maxLength;
        private int minLength;

        public PasswordAttribute(int maxLength, int minLength)
        {
            this.maxLength = maxLength;
            this.minLength = minLength;
        }

        public bool ContainsLowercase { get; set; }

        public bool ContainsUppercase { get; set; }

        public bool ContainsDigit { get; set; }

        public bool ContainsSpecialSymbol { get; set; }

        public override bool IsValid(object value)
        {
            string password = value.ToString();

            if (password.Length < this.minLength || password.Length > this.maxLength)
            {
                return false;
            }

            if (this.ContainsLowercase && !password.Any(c => char.IsLower(c)))
            {

                return false;
            }

            if (this.ContainsUppercase && !password.Any(c => char.IsUpper(c)))
            {
                return false;
            }

            if (this.ContainsDigit && !password.Any(c => char.IsDigit(c)))
            {
                return false;
            }

            return true;
        }
    }
}
