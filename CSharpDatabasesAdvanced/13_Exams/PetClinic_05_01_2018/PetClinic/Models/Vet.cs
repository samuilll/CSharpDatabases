using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.Models
{
   public class Vet
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Profession { get; set; }
        [Range(minimum:22,maximum:65)]
        public int Age { get; set; }
        [Required]
        [RegularExpression(@"\+359[0-9]{9}|0{1}[0-9]{9}")]
        public string PhoneNumber { get; set; }

        public ICollection<Procedure> Procedures { get; internal set; }
    }
}
