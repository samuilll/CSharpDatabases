using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using Travelling.Models;

namespace Travelling.ProcessData.Dtos
{
    [XmlType("Card")]
    public class CardDto
    {

        [Required]
        [MaxLength(128)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Range(minimum: 1, maximum: 120)]
        [XmlElement("Age")]
        public int Age { get; set; }

        [XmlElement("CardType"), DefaultValue(Travelling.Models.CardType.Normal)]
        public virtual CardType? CardType { get; set; }
    }
}
