using System;
using System.Collections.Generic;

namespace P_03ProductShop.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> BuiedProducts { get; set; }

        public virtual ICollection<Product> ProductsToSell { get; set; }

    }
}
