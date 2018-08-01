using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace P_04CarDealer.App.ModelsDto
{
    [XmlType("part")]
    public class PartDto
    {
        [Required]
        [XmlAttribute("name")]
        public string Name { get; set; }
        [Required]
        [XmlAttribute("price")]
        public decimal Price { get; set; }
        [Required]
        [XmlAttribute("quantity")]
        public int Quantity { get; set; }
        [XmlIgnore]
        public int SupplierId { get; set; }

        public bool ShouldSerializeQuantity()
        {
            return false;
        }
    }
}
