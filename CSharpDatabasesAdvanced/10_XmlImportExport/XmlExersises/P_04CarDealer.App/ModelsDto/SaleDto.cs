using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_04CarDealer.App.ModelsDto
{
    [XmlType("sale")]
    public class SaleDto
    {

        [XmlElement("car")]
        public SaleCarDto Car { get; set; }
        [XmlElement("customer-name")]
        public string CustomerName { get; set; }
        [XmlElement("discount")]
        public decimal Discount { get; set; }
        [XmlElement("price")]
        public decimal Price { get; set; }
        [XmlElement("price-with-discount")]
        public decimal PriceWithDiscount
        {
            get
            {
                return this.Discount != 0 ? (this.Price - this.Price * this.Discount) : this.Price;
            }
            set
            {
                this.PriceWithDiscount = value;
            }
        }

    }

    [XmlType("car")]
    public class SaleCarDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }
        [XmlAttribute("model")]
        public string Model { get; set; }
        [XmlAttribute("travelled-distance")]
        public double TravelledDistance { get; set; }
    }
}
