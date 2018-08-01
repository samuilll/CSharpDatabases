using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeamBuilder.Models.Validations;

namespace TeamBuilder.Models
{
    public class User
    {

        public User()
        {
            this.CreatedEvents = new List<Event>();
            this.CreatedTeams = new List<Team>();
            this.UserTeams = new List<UserTeam>();
            this.ReceivedInvitations = new List<Invitation>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:25,MinimumLength =3,ErrorMessage ="InvalidUsername")]
        public string Username { get; set; }
        [MaxLength(25)]
        public string FirstName { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }

        [Password(25,3,ContainsDigit =true,ContainsUppercase =true,ErrorMessage = "InvalidPass!")]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        public int? Age { get; set; }

        public bool IsDeleted { get; set; }

        public int TeamId { get; set; }

        public virtual ICollection<Event> CreatedEvents { get; set; }

        public virtual ICollection<UserTeam> UserTeams { get; set; }

        public virtual ICollection<Team> CreatedTeams { get; set; }

        public virtual ICollection<Invitation> ReceivedInvitations { get; set; }
    }
}
