using PhotoShare.Client.Core.Commands.Contracts;
using PhotoShare.Services;
using PhotoShare.Services.Contracts;
using System;

namespace PhotoShare.Client.Core.Commands
{
    public class LogoutCommand : ICommand
    {
        private const string successMessage = "User {0} successfully logged out!";
        private readonly IUserService userService;

        public LogoutCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] data)
        {
            if (!Session.HasLoggedUser())
            {
                throw new ArgumentException(ExeptionMessageHandler.FirstLogInExeption);
            }

            var username = Session.User.Username;

            Session.User = null;

            return string.Format(successMessage, username);
        }
    }
}
