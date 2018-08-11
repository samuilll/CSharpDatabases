using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Travelling.Models;

namespace Travelling.ProcessData.Dtos
{
    [XmlType("Trip")]
    public class TripForTicketDto
    {

        [Required]
        [XmlElement("OriginStation")]
        public string OriginStation { get; set; }

        [Required]
        [XmlElement("DestinationStation")]
        public string DestinationStation { get; set; }

        [Required]
        [XmlElement("DepartureTime")]
        public string DepartureTime { get; set; }

    }
}
