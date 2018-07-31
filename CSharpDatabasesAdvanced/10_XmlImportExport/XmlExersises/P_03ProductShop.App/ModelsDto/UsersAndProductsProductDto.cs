using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("product")]
   public class UsersAndProductsProductDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("price")]
        public decimal Price { get; set; }

    }
}
