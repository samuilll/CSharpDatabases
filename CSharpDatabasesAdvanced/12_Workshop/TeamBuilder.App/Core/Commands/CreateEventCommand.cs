using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Core.Commands
{
    using TeamBuilder.App.Commands.Contracts;
    using TeamBuilder.App.Dtos;
    using TeamBuilder.Services;
    using TeamBuilder.Services.Contracts;

    public class CreateEventCommand : ICommand
    {

        private IEventService eventService;

        public CreateEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public string Execute(params string[] commandArgs)
        {
            throw new ArgumentException();
        }
    }
}
