using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
  public  class LogoutCommand:ICommand
    {
        private IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] commandArgs)
        {
            if (commandArgs.Length!=0)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            var username = this.userService.Logout();

            return string.Format(Constants.SuccessMessages.LogOutSuccessfull, username);
        }
    }
}
