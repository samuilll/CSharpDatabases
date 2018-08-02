using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.App.Dtos;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class InviteToTeamCommand : ICommand
    {

        private ITeamService teamService;

        public InviteToTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            var teamName = args[0];

            var invitedUser = args[1];

           return this.teamService.InviteToTeam(teamName,invitedUser);

        }
    }
}
