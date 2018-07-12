using System.Collections.Generic;

namespace Cars.Data.Data.Models
{
    public class Engine
    {
        public int Id { get; set; }

        public double Capacity{ get; set; }

        public string Make { get; set; }

        public int HorsePower { get; set; }

        public int Cyllinders { get; set; }

        public FuelType FuelType { get; set; }

        public ICollection<Car> Cars { get; set; }

    }
}