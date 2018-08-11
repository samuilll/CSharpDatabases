using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Item")]
    public class ItemForOrderDto
    {
        [XmlElement("Name")]
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(minimum: 1, maximum: int.MaxValue)]
        [XmlElement("Quantity")]
        public int Quantity { get; set; }
    }
}