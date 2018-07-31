using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace P_04CarDealer.Models
{
    public class Car
    {
        [Required]
        public int Id { get; set; }
       [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public double TravelledDistance { get; set; }

        public virtual IEnumerable<PartCar> PartCars { get; set; }

        public virtual IEnumerable<Sale> Sales { get; set; }
    }
}
