using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace P_04CarDealer.Models
{
   public class Sale
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int CarId { get; set; }

        public virtual Car Car { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        public decimal Discount { get; set; }
    }
}
