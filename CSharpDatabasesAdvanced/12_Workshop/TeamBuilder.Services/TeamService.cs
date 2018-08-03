using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Services
{
    using AutoMapper;
    using Contracts;
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class TeamService : ITeamService
    {
        private IMapper mapper;

        private const string successfullAcceptInvitationMessage = "User {0} joined team {1}!";

        private const string successfullInvitationMessage = "Team {0} invited {1}!";

        private const string declineInvitationMessage = "Invite from {0} declined.";

        private const string kickOfTheTeamMessage = "User {0} was kicked from {1}!";

        private const string disbandMessage = "{0} has disbanded!";

        private const string addTeamToEvent = "Team {0} added for {1}!";


        public TeamService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public string AcceptInvite(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                Utility.LoginCheck();

                var currentTeam = context.Teams.SingleOrDefault(t => t.Name == teamName);

                Utility.ExistingTeamCheck(teamName, currentTeam);

                var currentUser = AuthenticationManager.GetCurrentUser(context);

                var invitation = currentUser.ReceivedInvitations.FirstOrDefault(i => i.IsActive && i.Team.Id == currentTeam.Id);

                Utility.ExistingInvitationCheck(teamName, invitation);

                invitation.IsActive = false;

                context.UserTeams.Add(new UserTeam
                {
                    Team = currentTeam,
                    User = currentUser
                });

                context.SaveChanges();

                return string.Format(successfullAcceptInvitationMessage, currentUser.Username, teamName);
            }
        }

        public TModel CreateTeam<TModel>(params string[] args)
        {
            var teamName = args[0];

            var acronym = args[1];

            string description;

            if (args.Length == 2)
            {
                description = null;
            }
            else
            {
                description = args[2];
            }

            using (var context = new TeamBuilderContext())
            {
                if (context.Teams.Any(t => t.Name == teamName))
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamExists, teamName));
                }

                if (!Validation.IsStringValid(acronym, 3, 3))
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidAcronym, acronym));
                }

                Utility.LoginCheck();

                var creatorId = AuthenticationManager.GetCurrentUser(context).Id;

                var team = new Team
                {
                    Name = teamName,
                    Acronym = acronym,
                    Description = description,
                    CreatorId = creatorId
                };

                Validation.Validate(team);

                context.Teams.Add(team);

                context.SaveChanges();

                var teamDto = this.mapper.Map<TModel>(team);

                return teamDto;
            }



        }

        public string InviteToTeam(string teamName, string invitedUserName)
        {
            using (var context = new TeamBuilderContext())
            {
                Utility.LoginCheck();

                var currentUser = AuthenticationManager.GetCurrentUser(context);

                var currentUsername = currentUser.Username;

                var currentTeam = context.Teams.FirstOrDefault(t => t.Name == teamName);
                var invitedUser = context.Users.FirstOrDefault(u => u.Username == invitedUserName);

                if (currentTeam == null || invitedUser == null)
                {
                    throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
                }

                var creatorOfTeamUsername = currentTeam.Creator.Username;

                var isCreatorOrMember = currentUsername == creatorOfTeamUsername || currentTeam.UserTeams.Any(ut => ut.UserId == currentUser.Id);
                var isAlreadyMember = currentTeam.UserTeams.Any(ut => ut.UserId == invitedUser.Id);

                if (!isCreatorOrMember || isAlreadyMember)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
                }

                if (currentUser.Id == invitedUser.Id)
                {
                    context.UserTeams.Add(new UserTeam
                    {
                        Team = currentTeam,
                        User = currentUser
                    });

                    context.SaveChanges();

                    return string.Format(successfullInvitationMessage, teamName, invitedUserName);
                }


                var alreadyInvited = context.Invitations.Any(i => i.InvitedUserId == invitedUser.Id && i.Team.Id==currentTeam.Id && i.IsActive == true);

                if (alreadyInvited)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
                }

                var invitation = new Invitation
                {
                    InvitedUserId = invitedUser.Id,
                    IsActive = true,
                    TeamId = currentTeam.Id
                };

                Validation.Validate(invitation);

                context.Invitations.Add(invitation);

                context.SaveChanges();

                return string.Format(successfullInvitationMessage, teamName, invitedUserName);

            }

        }

        public string DeclineInvite(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                Utility.LoginCheck();

                var currentTeam = context.Teams.SingleOrDefault(t => t.Name == teamName);

                Utility.ExistingTeamCheck(teamName, currentTeam);

                var currentUser = AuthenticationManager.GetCurrentUser(context);

                var invitation = currentUser.ReceivedInvitations.FirstOrDefault(i => i.IsActive && i.Team.Id == currentTeam.Id);

                if (invitation == null)
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound, teamName));
                }

                invitation.IsActive = false;

                context.SaveChanges();

                return string.Format(declineInvitationMessage, teamName);
            }
        }

        public string KickMember(string teamName, string userName)
        {
            using (var context = new TeamBuilderContext())
            {
                Utility.LoginCheck();

                var team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                Utility.ExistingTeamCheck(teamName, team);

                var userToKick = context.Users.FirstOrDefault(u => u.Username == userName);
                Utility.ExistingUserCheck(userName, userToKick);

                var isTeamMember = team.UserTeams.Any(ut => ut.User.Id == userToKick.Id);
                if (!isTeamMember)
                {
                    throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, userName, teamName));
                }

                var currentUser = AuthenticationManager.GetCurrentUser(context);

                Utility.IsCreatorCheck(team, currentUser);

                if (currentUser.Id == userToKick.Id)
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.CannotDeleteCreatorOfTheTeam);
                }

                var userTeam = team.UserTeams.FirstOrDefault(ut => ut.UserId == userToKick.Id);

                context.UserTeams.Remove(userTeam);

                context.SaveChanges();

                return string.Format(kickOfTheTeamMessage, userName, teamName);
            }
        }

        public string AddTeamTo(string eventName, string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                Utility.LoginCheck();

                var currentEvent = context
                    .Events
                    .Where(e => e.Name == eventName)
                    .OrderByDescending(e => e.StartDate)
                    .FirstOrDefault();
                Utility.ExistingEventCheck(eventName, currentEvent);

                var team = context.Teams.FirstOrDefault(t => t.Name == teamName);
                Utility.ExistingTeamCheck(teamName, team);

                var currentUser = AuthenticationManager.GetCurrentUser(context);

                Utility.IsCreatorCheck(team, currentUser);

                Utility.IsTeamAlreadyAddedCheck(currentEvent, team);

                context.TeamEvents.Add(new TeamEvent
                {
                    Event = currentEvent,
                    Team = team
                });

                context.SaveChanges();
            }

            return string.Format(addTeamToEvent, teamName, eventName);
        }

        public string Disband(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                Utility.LoginCheck();

                var team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                Utility.ExistingTeamCheck(teamName, team);

                var currentUser = AuthenticationManager.GetCurrentUser(context);

                Utility.IsCreatorCheck(team, currentUser);

                context.Teams.Remove(team);

                context.SaveChanges();
            }

            return string.Format(disbandMessage, teamName);
        }

        public string ShowTeam(string teamName)
        {
            using (var context = new TeamBuilderContext())
            {
                var team = context.Teams.FirstOrDefault(t => t.Name == teamName);

                Utility.ExistingTeamCheck(teamName, team);

                return team.ToString();
            }
        }
   
    }
}
