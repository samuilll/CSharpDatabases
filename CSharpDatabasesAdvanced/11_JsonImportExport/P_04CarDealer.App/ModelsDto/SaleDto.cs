using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace P_04CarDealer.App.ModelsDto
{
    public class SaleDto
    {
        public SaleCarDto Car { get; set; }

        public string CustomerName { get; set; }

        public decimal Discount { get; set; }

        public decimal Price { get; set; }

        public decimal PriceWithDiscount
        {
            get
            {
                return this.Discount == 0 ? this.Price :(this.Price - this.Price * this.Discount);
            }
            set
            {
                this.PriceWithDiscount = value;
            }
        }

    }

    public class SaleCarDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public double TravelledDistance { get; set; }
    }
}
