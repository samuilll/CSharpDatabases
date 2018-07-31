using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_03ProductShop.App.ModelsDto
{
    [XmlType("category")]
   public class CategoryByProductDto
    {
        [XmlAttribute("name")]
        public string  Name { get; set; }
        [XmlElement("products-count")]
        public int ProductsCount { get; set; }
        [XmlElement("average-price")]
        public decimal AveragePrice { get; set; }
        [XmlElement("total-revenue")]
        public decimal AllRevenue { get; set; }
    }
}
