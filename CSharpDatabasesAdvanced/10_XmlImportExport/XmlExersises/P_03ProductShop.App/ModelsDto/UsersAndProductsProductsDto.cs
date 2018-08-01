using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("sold-products")]
   public class UsersAndProductsProductsDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }
        [XmlElement("product")]
        public UsersAndProductsProductDto[] Products { get; set; }
    }
}
