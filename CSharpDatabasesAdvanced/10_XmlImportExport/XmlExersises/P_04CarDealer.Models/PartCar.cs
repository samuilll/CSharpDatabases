using System;
using System.Collections.Generic;
using System.Text;

namespace P_04CarDealer.Models
{
   public class PartCar
    {
        public int CarId { get; set; }

        public virtual Car Car { get; set; }

        public int PartId { get; set; }

        public virtual Part Part { get; set; }
    }
}
