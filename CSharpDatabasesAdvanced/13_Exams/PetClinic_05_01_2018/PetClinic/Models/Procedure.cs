using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace PetClinic.Models
{
  public  class Procedure
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }

        public Animal Animal { get; set; }


        public int VetId { get; set; }

        public Vet Vet { get; set; }

        [NotMapped]
        public decimal Cost
        {
            get
            {
                return this.ProcedureAnimalAids.Sum(paa => paa.AnimalAid.Price);
            }
        }
        [Required]
        public DateTime DateTime { get; set; }

        public ICollection<ProcedureAnimalAid> ProcedureAnimalAids { get; set; }
    }
}
