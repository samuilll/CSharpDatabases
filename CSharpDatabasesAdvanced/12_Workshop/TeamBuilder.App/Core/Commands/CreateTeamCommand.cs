using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.App.Dtos;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class CreateTeamCommand : ICommand
    {
        private const string successfullMessage = "Team {0} was created successfully!";

        private ITeamService teamService;

        public CreateTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        public string Execute(params string[] commandArgs)
        {
            if (commandArgs.Length != 2 && commandArgs.Length != 3)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            var teamDto = this.teamService.CreateTeam<TeamDto>(commandArgs);

            return string.Format(successfullMessage,teamDto.Name);
        }
    }
}
