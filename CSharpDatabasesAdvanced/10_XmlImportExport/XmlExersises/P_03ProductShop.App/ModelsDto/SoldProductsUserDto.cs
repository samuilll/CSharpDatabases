using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("user")]
  public  class SoldProductsUserDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlArray("sold-products")]
        public SoldProductsProductDto[] SoldProducts { get; set; }
    }
}
