using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Forum.App.Commands.Contracts;

namespace Forum.App
{
   public class Engine
    {
        private IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Run()
        {
            var databaseInitializer = serviceProvider.GetService<IDatabaseInitializerService>();

            databaseInitializer.InitiaizeDatabase();

            while (true)
            {
                Console.WriteLine("EnterCommand");

                var input = Console.ReadLine();

                var commandTokens = input.Split(' ');

                var commandName = commandTokens[0];

                var commandArgs = commandTokens.Skip(1).ToArray();

                try
                {
                    ICommand command = CommandParser.ParseCommand(serviceProvider, commandName);

                    string result = command.Execute(commandArgs);

                    Console.WriteLine(result);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
