using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class AcceptInviteCommand : ICommand
    {

        private ITeamService teamService;

        public AcceptInviteCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            var teamName = args[0];

            return this.teamService.AcceptInvite(teamName);
        }
    }
}
