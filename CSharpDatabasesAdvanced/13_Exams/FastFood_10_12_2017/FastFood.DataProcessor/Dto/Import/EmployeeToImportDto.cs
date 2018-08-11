using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.DataProcessor.Dto.Import
{
   public class EmployeeToImportDto
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [Range(minimum: 15, maximum: 80)]
        public int Age { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Position { get; set; }
    }
}
