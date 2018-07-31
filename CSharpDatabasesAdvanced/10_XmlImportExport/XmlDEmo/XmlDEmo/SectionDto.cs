using System.Xml.Serialization;

namespace XmlDEmo
{
    [XmlType("Section")]
    public class SectionDto
    {
        [XmlElement("SectionName")]
        public string Name { get; set; }

        [XmlArrayItem("Book")]
        public BookDto[] Books { get; set; }
    }
}