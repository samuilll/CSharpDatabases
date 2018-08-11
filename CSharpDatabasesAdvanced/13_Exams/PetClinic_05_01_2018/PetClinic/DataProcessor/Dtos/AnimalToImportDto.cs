using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.Dtos
{
   public class AnimalToImportDto
    {
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Type { get; set; }
        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        public PassportToImportDto Passport { get; set; }

    }
}
