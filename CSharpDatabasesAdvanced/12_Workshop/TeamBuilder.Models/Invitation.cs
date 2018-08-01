using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Models
{
   public class Invitation
    {
        public Invitation()
        {
            this.IsActive = true;
        }

        public int Id { get; set; }

        public int InvitedUserId { get; set; }

        public virtual User User { get; set; }

        public int TeamId { get; set; }

        public virtual Team Team { get; set; }

        public bool IsActive { get; set; }
    }
}
