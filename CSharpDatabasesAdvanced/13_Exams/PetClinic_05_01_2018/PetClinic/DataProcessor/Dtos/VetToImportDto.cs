using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos
{
    [XmlType("Vet")]
   public class VetToImportDto
    {
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        [XmlElement]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [XmlElement]
        public string Profession { get; set; }
        [Range(minimum: 22, maximum: 65)]
        [XmlElement]
        public int Age { get; set; }
        [Required]
        [RegularExpression(@"\+359[0-9]{9}|0{1}[0-9]{9}")]
        [XmlElement]
        public string PhoneNumber { get; set; }
    }
}

//<Vets>
//    <Vet>
//		<Name>Michael Jordan</Name>
//		<Profession>Emergency and Critical Care</Profession>
//        <Age>45</Age>
//        <PhoneNumber>0897665544</PhoneNumber>
//    </Vet>
