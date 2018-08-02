using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P_04CarDealer.Models
{
  public  class Part
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }

        public virtual IEnumerable<PartCar> PartCars { get; set; }
    }
}
