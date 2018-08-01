using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.Services.Contracts
{
   public interface IEventService
    {
        TModel CreateEvent<TModel>(params string[] arguments);
    }
}
