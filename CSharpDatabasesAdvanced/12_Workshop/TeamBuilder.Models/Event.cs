﻿

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Validations;

namespace TeamBuilder.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<TeamEvent> EventTeams { get; set; }
    }
}
