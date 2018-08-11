using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos
{
    [XmlType("Procedure")]
   public class ProcedureToExportDto
    {
        [XmlElement]
        public string Passport { get; set; }
        [XmlElement]
        public string OwnerNumber { get; set; }
        [XmlElement]
        public string DateTime { get; set; }
   
        [XmlArray]
        public AnimalAidToExportDto[] AnimalAids { get; set; }

        [XmlElement]
        public decimal TotalPrice
        {
            get
            {
                return this.AnimalAids.Sum(aa => aa.Price);
            }
            set
            {

            }
        }
    }
}
