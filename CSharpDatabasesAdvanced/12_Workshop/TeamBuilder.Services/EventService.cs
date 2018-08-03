using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Services
{
    using AutoMapper;
    using Contracts;
    using System.Globalization;
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    public class EventService : IEventService
    {
        private IMapper mapper;

        public EventService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TModel CreateEvent<TModel>(params string[] args)
        {
            var name = args[0];

            var description = args[1];

            var isValidStartDate = DateTime.TryParseExact(string.Concat(args[2]," ",args[3]), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate);

            var isValidEndDate = DateTime.TryParseExact(string.Concat(args[4], " ", args[5]), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate);

            if (!isValidEndDate || !isValidStartDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (startDate.CompareTo(endDate)>-1)
            {
                throw new ArgumentException(Constants.ErrorMessages.StartDateMustPrecedeEndDate);
            }

            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            using (var context = new TeamBuilderContext())
            {

            var creatorId = AuthenticationManager.GetCurrentUser(context).Id;

            var currentEvent = new Event()
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                CreatorId = creatorId
            };

            Validation.Validate(currentEvent);

            
                context.Events.Add(currentEvent);

                context.SaveChanges();

                var eventDto = this.mapper.Map<TModel>(currentEvent);

                return eventDto;

            }


        }

        public string ShowEvent(string eventName)
        {
            using (var context = new TeamBuilderContext())
            {

                var currentEvent = context
                    .Events
                    .Where(e => e.Name == eventName)
                    .OrderByDescending(e => e.StartDate)
                    .FirstOrDefault();

                Utility.ExistingEventCheck(eventName, currentEvent);

                return currentEvent.ToString();
            }
        }
    }
}
