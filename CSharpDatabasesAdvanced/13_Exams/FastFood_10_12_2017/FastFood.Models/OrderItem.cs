using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.Models
{
   public class OrderItem
    {
        [Required]
        public int OrderId { get; set; }

        public Order Order { get; set; }
        [Required]
        public int ItemId { get; set; }

        public Item Item { get; set; }

        [Required]
        [Range(minimum:1,maximum:int.MaxValue)]
        public int Quantity { get; set; }
    }
}
