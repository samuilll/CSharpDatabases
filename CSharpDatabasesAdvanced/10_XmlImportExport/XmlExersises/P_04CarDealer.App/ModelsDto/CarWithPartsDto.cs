using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_04CarDealer.App.ModelsDto
{
    [XmlType("car")]
  public  class CarWithPartsDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }
        [XmlAttribute("model")]
        public string Model { get; set; }
        [XmlAttribute("travelled-distance")]
        public double TravelledDistance { get; set; }
        [XmlElement("part")]
        public PartDto[] Parts { get; set; }
    }
}
