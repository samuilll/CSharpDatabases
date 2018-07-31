using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P_04CarDealer.Models
{
   public class Supplier
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsImporter { get; set; }

        public virtual IEnumerable<Part> Parts { get; set; }
    }
}
