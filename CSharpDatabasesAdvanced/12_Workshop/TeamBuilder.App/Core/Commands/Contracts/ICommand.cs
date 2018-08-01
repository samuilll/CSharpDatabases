using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Commands.Contracts
{
   public interface  ICommand
    {
        string Execute(params string[] commandArgs); 
    }
}
