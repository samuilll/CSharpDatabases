using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("users")]
  public  class UsersAndProductsUsersDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }
        [XmlElement("user")]
        public UsersAndProductsUserDto[] Users { get; set; }
    }
}
