using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("user")]
   public class UserDto
    {
        [XmlAttribute("firstName")]
        public string FirstName { get; set; }

        [MinLength(3)]
        [XmlAttribute("lastName")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }
    }
}
