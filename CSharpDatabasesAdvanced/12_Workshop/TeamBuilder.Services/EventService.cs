using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Services
{
    using AutoMapper;
    using Contracts;

    public class EventService : IEventService
    {
        private IMapper mapper;

        public EventService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public TModel CreateEvent<TModel>(params string[] arguments)
        {
            throw new NotImplementedException();
        }
    }
}
