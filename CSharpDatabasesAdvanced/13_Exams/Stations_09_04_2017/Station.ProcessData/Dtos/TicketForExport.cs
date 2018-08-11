using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Travelling.ProcessData.Dtos
{
    [XmlType("Ticket")]
  public  class TicketForExport
    {
        [XmlElement("OriginStation")]
        public  string OriginStation { get; set; }

        [XmlElement("DestinationStation")]
        public  string DestinationStation { get; set; }

        [XmlElement("DepartureTime")]
        public string DepartureTime { get; set; } 

        [XmlIgnore]
        public TripForTicketDto Trip { get; set; }
    }
}
