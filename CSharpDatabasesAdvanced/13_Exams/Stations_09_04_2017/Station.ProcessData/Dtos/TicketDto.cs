using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Travelling.ProcessData.Dtos
{
    [XmlType("Ticket")]
  public  class TicketDto
    {

        [Required]
        public TripForTicketDto Trip { get; set; }

        [Required]
        [Range(minimum:0,maximum:int.MaxValue)]

        [XmlAttribute("price")]
        public decimal Price { get; set; }

        [XmlAttribute("seat")]
        public string Seat { get; set; }

        [XmlElement("Card")]
        public CardForTicketDto Card { get; set; }

    // <Trip>
    //  <OriginStation>Rzhev</OriginStation>
    //  <DestinationStation>Chateaubelair</DestinationStation>
    //  <DepartureTime>19/07/2016 13:33</DepartureTime>
    //</Trip>
    //<Card Name = "Joan Henderson"
    }
}
