using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Dtos
{
  public  class EventDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }
    }
}
