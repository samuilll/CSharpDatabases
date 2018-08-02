using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class KickMemberCommand : ICommand
    {

        private ITeamService teamService;

        public KickMemberCommand(ITeamService teamService)
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

            var userName = args[1];

            return this.teamService.KickMember(teamName,userName);
        }
    }
}
