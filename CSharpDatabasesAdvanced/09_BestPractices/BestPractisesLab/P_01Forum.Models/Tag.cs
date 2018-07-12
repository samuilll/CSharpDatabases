
using System.Collections.Generic;

namespace Forum.Data.Models
{
  public  class Tag
    {
        public Tag()
        {
        }

        public Tag(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PostTag> PostsTags { get; set; }
    }
}
