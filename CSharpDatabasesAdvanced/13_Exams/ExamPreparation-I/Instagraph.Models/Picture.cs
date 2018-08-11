using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Instagraph.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false)]
        public string Path { get; set; }
        [Required]
        [Range(minimum:1,maximum:int.MaxValue)]
        public decimal Size { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
