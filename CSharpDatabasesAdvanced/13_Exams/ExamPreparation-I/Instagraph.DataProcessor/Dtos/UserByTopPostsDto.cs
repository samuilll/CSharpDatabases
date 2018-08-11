using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dtos
{
    [XmlType("user")]
  public  class UserByTopPostsDto
    {
        [XmlElement("Username")]
        public string Username { get; set; }
        [XmlElement("MostComments")]
        public int MostComments { get; set; }
    }
}
