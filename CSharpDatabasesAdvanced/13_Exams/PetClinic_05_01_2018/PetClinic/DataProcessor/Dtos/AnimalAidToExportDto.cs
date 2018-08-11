using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos
{
    [XmlType("AnimalAid")]
   public class AnimalAidToExportDto
    {
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public decimal Price { get; set; }
    }
}
