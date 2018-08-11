using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
  public   class Animal
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:20,MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string Type { get; set; }
        [Range(1,int.MaxValue)]
        public int Age { get; set; }
        [Required]
        public string PassportSerialNumber { get; set; }

        public Passport Passport { get; set; }

        public ICollection<Procedure> Procedures { get; internal set; }
    }
}
