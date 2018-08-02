using System;
using System.Collections.Generic;
using System.Text;

namespace P_03ProductShop.Models
{
   public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
