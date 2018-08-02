using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
   public class UserDto
    {
        public string FirstName { get; set; }

        [MinLength(3)]
        public string LastName { get; set; }

        public string Age { get; set; }
    }
}
