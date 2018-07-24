using PhotoShare.Client.Core.Commands.Contracts;
using PhotoShare.Services;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private const string successMessage = "User {0} successfully logged in!";
        private readonly IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] data)
        {
            var username = data[0];
            var password = data[1];

            var user = this.userService.GetByUsernameAndPassword(username, password);

            if (Session.HasLoggedUser())
            {
                throw new ArgumentException(ExeptionMessageHandler.FirstLogOutExeption);
            }

            Session.User = user;

            return string.Format(successMessage,username);
        }
    }
}
