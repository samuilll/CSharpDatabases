using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Commands.Contracts;
using TeamBuilder.Services;
using TeamBuilder.Services.Contracts;

namespace TeamBuilder.App.Core.Commands
{
  public  class LoginCommand:ICommand
    {
        private IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] commandArgs)
        {
            if (commandArgs.Length != 2)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            var username = commandArgs[0];

            var password = commandArgs[1];

            this.userService.Login(username,password);

            return string.Format(Constants.SuccessMessages.LogInSuccessfull, username);
        }
    }
}
