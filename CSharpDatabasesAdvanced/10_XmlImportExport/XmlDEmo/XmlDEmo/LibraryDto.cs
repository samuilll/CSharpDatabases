using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XmlDEmo
{
    [XmlRoot("Library")]
    [XmlType("Library")]
    public class LibraryDto
    {
        [XmlElement("Name")]
        public string LibraryName { get; set; }

        [XmlElement("Section")]
        public SectionDto Section { get; set; }

        [XmlIgnore]
        public decimal CardPrice { get; set; }
    }
}
