using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace P_04CarDealer.App.ModelsDto
{
    [XmlType("customer")]
    public class CustomerDto
    {
        [Required]
        [XmlAttribute("name")]
        public string Name { get; set; }
        [Required]
        [XmlElement("birth-date")]
        public DateTime BirthDay { get; set; }
        [Required]
        [XmlElement("is-young-driver")]
        public bool IsYoungDriver { get; set; }
    }
}
