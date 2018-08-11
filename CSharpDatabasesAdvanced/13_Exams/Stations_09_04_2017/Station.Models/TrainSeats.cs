using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travelling.Models
{
   public class TrainSeats
    {
        [Required]
        public int TrainId { get; set; }

        public virtual Train Train { get; set; }

        [Required]
        public int SeatingClassId { get; set; }

        public virtual SeatingClass SeatingClass { get; set; }

        [Required]
        [Range(minimum:0,maximum:int.MaxValue)]
        public int Quantity { get; set; }
    }
}
