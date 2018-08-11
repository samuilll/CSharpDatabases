using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travelling.Models
{
   public class SeatingClass
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Abbreviation { get; set; }

        public virtual ICollection<TrainSeats> TrainSeatClasses { get; set; }
    }
}
