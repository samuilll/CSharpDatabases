using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travelling.Models
{
   public class Train
    {
        public int Id { get; set; }

        [Required]
        public string TrainNumber { get; set; }

        public TrainType? Type { get; set; }

        public virtual ICollection<TrainSeats> TrainSeatClasses { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }
    }
}
