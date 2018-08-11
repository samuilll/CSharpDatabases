using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos
{
    [XmlType("AnimalAid")]
  public  class AnimalAidFoProcedureDto
    {
        [XmlElement]
        public string Name { get; set; }
    }
}
