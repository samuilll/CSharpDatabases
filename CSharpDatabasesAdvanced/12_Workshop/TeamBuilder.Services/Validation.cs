using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamBuilder.Services
{
  public static  class Validation
    {
        public static void Validate(object entity)
        {
            var validationContext = new ValidationContext(entity);

            Validator.ValidateObject(
                entity,
                validationContext,
                validateAllProperties: true);
        }

        public static bool IsStringValid(string value,int minLength, int maxLength)
        {
            if (value.Length<minLength || value.Length>maxLength)
            {
                return false;
            }
            return true;
        }
    }
}
