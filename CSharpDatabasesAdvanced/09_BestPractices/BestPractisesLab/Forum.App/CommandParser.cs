using Forum.App.Commands.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace Forum.App
{
    internal class CommandParser
    {
        public static ICommand ParseCommand(IServiceProvider serviceProvider,string commandName)
        {
            Type commandType = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t=>t.GetInterfaces().Contains(typeof(ICommand)))
                .SingleOrDefault(t => t.Name == commandName+"Command");

            if (commandType==null)
            {
                throw new InvalidOperationException("Invalid Command");
            }

            var constructor = commandType.GetConstructors().First();

            var constructorParameters = constructor.GetParameters()
                .Select(pi => pi.ParameterType).ToArray();

            var services = constructorParameters.Select(p => serviceProvider.GetService(p)).ToArray();

            var command = Activator.CreateInstance(commandType,services);

            return (ICommand)command;
        }
    }
}