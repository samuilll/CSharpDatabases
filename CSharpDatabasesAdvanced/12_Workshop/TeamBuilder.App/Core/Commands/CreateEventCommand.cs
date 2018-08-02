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
        private const string successfullMessage = "Event {0} was created successfully!"; 

        private IEventService eventService;

        public CreateEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public string Execute(params string[] commandArgs)
        {
            if (commandArgs.Length!= 6)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            EventDto eventDto = this.eventService.CreateEvent<EventDto>(commandArgs);

            return string.Format(successfullMessage,eventDto.Name);
        }
    }
}
