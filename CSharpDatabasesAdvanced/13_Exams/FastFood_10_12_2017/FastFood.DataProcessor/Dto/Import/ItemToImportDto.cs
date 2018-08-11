using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.DataProcessor.Dto.Import
{
   public class ItemToImportDto
    {
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Category { get; set; }
        [Required]
        [Range(minimum: 0.01, maximum: double.MaxValue)]
        public decimal Price { get; set; }
    }
}
