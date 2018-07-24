namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using System;

    public class ExitCommand:ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("Good Bye!");
            Environment.Exit(0);
           return  "Bye-bye!";
        }

    }
}
