using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.Dtos
{
  public  class PassportToImportDto
    {
        [Required]
        [RegularExpression("[A-Za-z]{7}[0-9]{3}")]
        public string SerialNumber { get; set; }
        [Required]
        [RegularExpression(@"\+359[0-9]{9}|0{1}[0-9]{9}")]
        public string OwnerPhoneNumber { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string OwnerName { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}
