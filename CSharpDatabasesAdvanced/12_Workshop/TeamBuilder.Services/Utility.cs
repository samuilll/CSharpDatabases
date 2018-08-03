using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.Models;

namespace TeamBuilder.Services
{
  public static  class Utility
    {
        public static void ExistingUserCheck(string userName, User userToKick)
        {
            if (userToKick == null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, userName));
            }
        }

        public static void LoginCheck()
        {
            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
        }

        public static void ExistingInvitationCheck(string teamName, Invitation invitation)
        {
            if (invitation == null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
            }
        }

        public static void ExistingTeamCheck(string teamName, Team currentTeam)
        {
            if (currentTeam == null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }
        }

        public static void IsCreatorCheck(Team team, User currentUser)
        {
            var isCreator = currentUser.Id == team.CreatorId;

            if (!isCreator)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }
        }

        public static void IsTeamAlreadyAddedCheck(Event currentEvent, Team team)
        {
            var isEventAlreadyAdded = currentEvent.EventTeams.Any(et => et.Team.Id == team.Id);

            if (isEventAlreadyAdded)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
            }
        }

        public static void ExistingEventCheck(string eventName, Event currentEvent)
        {
            if (currentEvent == null)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
            }
        }
    }
}
