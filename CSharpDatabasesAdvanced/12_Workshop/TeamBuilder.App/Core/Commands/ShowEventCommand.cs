using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class ShowEventCommand : ICommand
    {
        private IEventService eventService;

        public ShowEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public string Execute(params string[] args)
        {
            if (args.Length != 1)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            var eventName = args[0];

            return this.eventService.ShowEvent(eventName);
        }
    }
}
