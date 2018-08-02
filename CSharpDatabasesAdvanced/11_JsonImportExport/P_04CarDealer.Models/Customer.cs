using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P_04CarDealer.Models
{
  public  class Customer
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public bool IsYoungDriver { get; set; }

        public virtual IEnumerable<Sale> Sales { get; set; }
    }
}
