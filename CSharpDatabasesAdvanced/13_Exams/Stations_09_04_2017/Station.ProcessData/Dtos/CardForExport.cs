using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Travelling.ProcessData.Dtos
{
    [XmlType("Card")]
    public class CardForExport
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlArray("Tickets")]
        public TicketForExport[] Tickets { get; set; }
    }
}
