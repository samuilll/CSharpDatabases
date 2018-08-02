using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;

namespace TeamBuilder.App.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(params string[] commandArgs)
        {
            Environment.Exit(0);

            return string.Empty;
        }
    }
}
