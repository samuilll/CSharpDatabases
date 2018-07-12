using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Data.Data.Models
{
 public   class Car
    {
        
        public Car()
        {
            this.CarDealerships = new List<CarDealership>();
        }

        public int Id { get; set; }

        public int MakeId { get; set; }
        public Make Make { get; set; }

        public int EngineId { get; set; }

        public Engine Engine { get; set; }

        public int Doors { get; set; }

        public Transmission Transmission { get; set; }

        public DateTime ProductionYear { get; set; }

        public ICollection<CarDealership> CarDealerships { get; set; }

        public int? LicensePlateId { get; set; }
        public LicensePlate Plate { get; set; }
    }
}
