using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Travelling.Models
{
  public  class Ticket
    {
        public int Id { get; set; }

        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        [Required]
        [Range(minimum:0,maximum:int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(8)]
        public string SeatingPlace { get; set; }

        public int? PersonalCardId { get; set; }

        public virtual CustomerCard PersonalCard { get; set; }
    }
}
