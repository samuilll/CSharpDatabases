using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dtos
{
    [XmlType("post")]
  public  class PostForCommentDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
