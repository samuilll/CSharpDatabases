using Instagraph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dtos
{
    [XmlType("post")]
    public class PostForImportDto
    {
        [Required]
        [XmlElement("caption")]
        public string Caption { get; set; }
        [XmlElement("user")]
        public string UserUserName { get; set; }
        [XmlElement("picture")]
        public string PicturePath { get; set; }
    }
}
