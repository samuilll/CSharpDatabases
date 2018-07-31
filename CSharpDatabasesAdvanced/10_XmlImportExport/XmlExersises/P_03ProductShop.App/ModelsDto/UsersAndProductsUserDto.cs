using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("user")]
   public class UsersAndProductsUserDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }
        [XmlAttribute("last-name")]
        public string LastName { get; set; }
        [XmlAttribute("age-name")]
        public string Age { get; set; }
        [XmlElement("sold-products")]
        public UsersAndProductsProductsDto SoldProducts { get; set; }
    }
}
