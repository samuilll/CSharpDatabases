using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Dtos
{
   public class TeamDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Acronym { get; set; }

        public int CreatorId { get; set; }
    }
}
