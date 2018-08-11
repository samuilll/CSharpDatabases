using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travelling.Models
{
   public class Station
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Town { get; set; }

        public virtual ICollection<Trip> Departures { get; set; }

        public virtual ICollection<Trip> Arrivals { get; set; }
    }
}
