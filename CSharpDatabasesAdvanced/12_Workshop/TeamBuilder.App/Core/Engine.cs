namespace TeamBuilder.App
{
    using System;
    using System.IO;
    using System.Text;
    using TeamBuilder.App.Contracts;
    using TeamBuilder.Services.Contracts;



    public class Engine
    {
        private IServiceProvider serviceProvider;

        private StringBuilder sb = new StringBuilder();

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

            string result=string.Empty;

            while (result!=null)
            {

                try
                {
                    Console.WriteLine("Please choose command:");

                    string input = Console.ReadLine().Trim();

                    Console.ForegroundColor = ConsoleColor.Red;
                    string[] data = input.Split(' ');

                    result = this.commandDispatcher.DispatchCommand(data);

                    Console.WriteLine(result);
    
                    sb.AppendLine(result);
                    Console.ResetColor();
                }
                catch (InvalidOperationException e)
                {
                    sb.AppendLine(e.Message);
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                catch (ArgumentException e)
                {
                    sb.AppendLine(e.Message);
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
                catch (Exception e)
                {
                    sb.AppendLine(e.Message);
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                }
            }

            File.WriteAllText("../output.txt",sb.ToString());
        }
    }
}
