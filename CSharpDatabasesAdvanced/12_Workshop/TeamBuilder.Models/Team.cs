using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TeamBuilder.Models
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [MaxLength(32)]
        public string Description { get; set; }
        [Required]
        [StringLength(maximumLength:3,MinimumLength =3,ErrorMessage ="Invalid description")]
        public string Acronym  { get; set; }

        public int CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public virtual ICollection<UserTeam> UserTeams { get; set; }

        public virtual ICollection<TeamEvent> EventTeams { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"{this.Name} {this.Acronym}");

            sb.AppendLine("Members:");

            foreach (var tm in this.UserTeams)
            {
                sb.AppendLine($"{tm.User.Username}");
            }
            return sb.ToString().TrimEnd('\r','\n');
        }
    }
}
