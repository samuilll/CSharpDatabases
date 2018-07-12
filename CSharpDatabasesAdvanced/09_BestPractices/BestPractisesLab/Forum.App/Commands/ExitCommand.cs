
using Forum.App.Commands.Contracts;
using System;

namespace Forum.App.Commands
{
    class ExitCommand : ICommand
    {
        public string Execute(params string[] arguments)
        {
            Environment.Exit(0);

            return string.Empty;
        }
    }
}
