using PhotoShare.Client.Core.Commands.Contracts;
using PhotoShare.Services;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Client.Core.Commands
{
    public class MakeFriendsCommand : ICommand
    {
        private const string successMessage = "Friend {1} added to {0}!";
        private IUserService userService;

        public MakeFriendsCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] data)
        {
            var username1 = data[0];
            var username2 = data[1];

            bool hasCredentials = Session.HasCredentials(username1);

            if (!hasCredentials)
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            this.userService.AddFriend(username1, username2);
            this.userService.AcceptFriend(username2, username1);

            return string.Format(successMessage, username1, username2);
        }
    }
}
