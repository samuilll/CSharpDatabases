using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Travelling.ProcessData.Dtos
{
    [XmlType("Card")]
    public class CardForTicketDto
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
    }
}
