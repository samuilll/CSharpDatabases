using P_04CarDealer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace P_04CarDealer.App.ModelsDto
{
    [XmlType("car")]
   public class CarDto
    {
        [Required]
        [XmlElement("make")]
        public string Make { get; set; }
        [Required]
        [XmlElement("model")]
        public string Model { get; set; }
        [Required]
        [XmlElement("travelled-distance")]
        public double TravelledDistance { get; set;}

    }
}
