using System.ComponentModel.DataAnnotations;

namespace Travelling.Models
{
    public class CustomerCard
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Range(minimum:0,maximum:120)]
        public int Age { get; set; }

        public virtual CardType CardType { get; set; }
    }
}