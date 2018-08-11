using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dtos
{
    [XmlType("comment")]
  public  class CommentToImportDto
    {
        [Required]
        [XmlElement("content")]
        public string Content { get; set; }
        [Required]
        [XmlElement("user")]
        public string User { get; set; }
        [Required]
        [XmlElement("post")]
        public PostForCommentDto Post { get; set; }
    }
}

