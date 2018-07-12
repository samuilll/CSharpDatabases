using System.Collections.Generic;

namespace Forum.Data.Models
{
   public class Category
    {
        public Category()
        {
        }

        public Category(string Name)
        {
            this.Name = Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
