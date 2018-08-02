using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class AddTeamToCommand : ICommand
    {

        private ITeamService teamService;

        public AddTeamToCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }
            var eventName = args[0];
            var teamName = args[1];

            return this.teamService.AddTeamTo(eventName,teamName);
        }
    }
}
