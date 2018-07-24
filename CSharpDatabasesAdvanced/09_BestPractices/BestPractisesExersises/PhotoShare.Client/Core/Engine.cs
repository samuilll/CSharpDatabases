namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Contracts;
    using System;

    public class Engine
    {
        private IServiceProvider serviceProvider;

        private readonly CommandDispatcher commandDispatcher;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.commandDispatcher = (CommandDispatcher)this.serviceProvider.GetService(typeof(ICommandDispatcher));
        }

        public void Run()
        {
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
