using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("category")]
   public class CategoryDto
    {
        [StringLength(maximumLength:15,MinimumLength =3)]
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
