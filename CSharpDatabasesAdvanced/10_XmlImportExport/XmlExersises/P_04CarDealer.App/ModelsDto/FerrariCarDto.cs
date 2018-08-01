using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_04CarDealer.App.ModelsDto
{
    [XmlType("car")]
    public class FerrariCarDto
    {
     
            [XmlAttribute("id")]
            public int Id { get; set; }
            [XmlAttribute("model")]
            public string Model { get; set; }
            [XmlAttribute("travelled-distance")]
        public double TravelledDistance { get; set; }
    }
}
