using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Order")]
  public  class OrderToImportDto
    {
        [Required]
        [XmlElement("Customer")]
        public string Customer { get; set; }
        [Required]
        [XmlElement("DateTime")]
        public string DateTime { get; set; }
        [Required]
        [XmlElement("Type")]
        public string Type { get; set; }

        [Required]
        public string Employee { get; set; }

        [XmlArray("Items")]
        public ItemForOrderDto[] OrderItems { get; set; }
    }
}


//<Orders>
//  <Order>
//    <Customer>Garry</Customer>
//    <Employee>Maxwell Shanahan</Employee>
//    <DateTime>21/08/2017 13:22</DateTime>
//    <Type>ForHere</Type>
//    <Items>
//      <Item>
//        <Name>Quarter Pounder</Name>
//        <Quantity>2</Quantity>
//      </Item>
//      <Item>
//        <Name>Premium chicken sandwich</Name>
//        <Quantity>2</Quantity>
//      </Item>
//      <Item>
//        <Name>Chicken Tenders</Name>
//        <Quantity>4</Quantity>
//      </Item>
//      <Item>
//        <Name>Just Lettuce</Name>
//        <Quantity>4</Quantity>
//      </Item>
//    </Items>
//  </Order>