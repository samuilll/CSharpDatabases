using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Core.Commands
{
    using TeamBuilder.App.Commands.Contracts;
    using TeamBuilder.App.Dtos;
    using TeamBuilder.Services;
    using TeamBuilder.Services.Contracts;

    public class RegisterUserCommand : ICommand
    {

        private IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] commandArgs)
        {
            if (commandArgs.Length != 7)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidArgumentsCount);
            }

            var userDto = this.userService.RegisterUser<UserDto>(commandArgs);

            return string.Format(Constants.SuccessMessages.RegistrationSuccessfull, userDto.Username);
        }
    }
}
