namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;

    public class AcceptFriendCommand:ICommand
    {

        private readonly IUserService userService;

        private const string successMessage = "{0} accepted {1} as a friend";

        public AcceptFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username1 = data[0];
            var username2 = data[1];

            bool hasCredentials = Session.HasCredentials(username1);

            if (!hasCredentials)
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            this.userService.AcceptFriend(username1, username2);

            return string.Format(successMessage, username1, username2);
        }
    }
}
