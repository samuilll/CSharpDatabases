namespace Employees.App
{
    using Employees.Contracts;
    using Employees.Services.Contracts;
    using Employess.App;
    using System;

    public class Engine
    {
        private IServiceProvider serviceProvider;

        private readonly CommandDispatcher commandDispatcher;

        private readonly IDatabaseInitializeService databaseInitializeService;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.commandDispatcher = (CommandDispatcher)this.serviceProvider.GetService(typeof(ICommandDispatcher));
            this.databaseInitializeService= (IDatabaseInitializeService)this.serviceProvider.GetService(typeof(IDatabaseInitializeService));
        }

        public void Run()
        {
            this.databaseInitializeService.InitializeDatabase();

            while (true)
            {
                try
                {
                    Console.WriteLine("Please choose command:");

                    string input = Console.ReadLine().Trim();

                    Console.ForegroundColor = ConsoleColor.Red;
                    string[] data = input.Split(' ');

                    string result = this.commandDispatcher.DispatchCommand(data);

                    Console.WriteLine(result);
                    Console.ResetColor();
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
            }
        }
    }
}
