using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.Dtos
{
    [XmlType("Procedure")]
  public  class ProcedureToImportDto
    {
        [XmlElement]
        [Required]
        public string Vet { get; set; }

        [XmlElement]
        [Required]
        public string Animal { get; set; }

        [Required]
        [XmlElement]
        public string DateTime { get; set; }

        [XmlArray("AnimalAids")]
        public AnimalAidFoProcedureDto[] AnimalAids { get; set; }
    }
}


 //<Procedure>
 //       <Vet>Niels Bohr</Vet>
 //       <Animal>acattee321</Animal>
	//	<DateTime>14-01-2016</DateTime>
 //       <AnimalAids>
 //           <AnimalAid>
 //               <Name>Nasal Bordetella</Name>
 //           </AnimalAid>
 //           <AnimalAid>
 //               <Name>Internal Deworming</Name>
 //           </AnimalAid>
 //           <AnimalAid>
 //               <Name>Fecal Test</Name>
 //           </AnimalAid>
 //       </AnimalAids>
 //   </Procedure>