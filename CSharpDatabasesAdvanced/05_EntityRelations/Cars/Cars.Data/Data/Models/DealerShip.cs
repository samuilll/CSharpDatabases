using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Data.Models
{
  public  class Dealership
    {
      
        public Dealership()
        {
            this.DealershipCars = new List<CarDealership>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CarDealership> DealershipCars { get; set; }
    }
}
