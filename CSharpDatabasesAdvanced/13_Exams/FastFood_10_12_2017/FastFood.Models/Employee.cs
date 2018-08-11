using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
	public class Employee
	{
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:30,MinimumLength =3)]
        public string Name { get; set; }
        [Required]
        [Range(minimum:15,maximum:80)]
        public int Age { get; set; }
        [Required]
        public int PositionId { get; set; }

        public Position Position { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}