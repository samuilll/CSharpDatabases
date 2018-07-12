using System.Collections.Generic;

namespace Cars.Data.Data.Models
{
    public class Make
    {
        public Make()
        {
            this.Cars = new List<Car>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}