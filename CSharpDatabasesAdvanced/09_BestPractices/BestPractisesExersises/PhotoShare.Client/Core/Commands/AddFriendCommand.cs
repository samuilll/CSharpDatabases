namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;

    public class AddFriendCommand:ICommand
    {
        private const string successMessage = "Friend {1} added to {0}";

        private  readonly IUserService userService;

        public AddFriendCommand(IUserService userService)
        {
            this.userService = userService;
        }


        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            var username1 = data[0];
            var username2 = data[1];

            bool hasCredentials = Session.HasCredentials(username1);

            if (!hasCredentials)
            {
                throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
            }


            this.userService.AddFriend(username1, username2);

            return string.Format(successMessage, username1, username2);

        }
    }
}
