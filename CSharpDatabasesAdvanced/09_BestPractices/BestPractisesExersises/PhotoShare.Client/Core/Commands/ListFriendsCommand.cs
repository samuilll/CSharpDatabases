namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services.Contracts;
    using System;

    public class ListFriendsCommand :ICommand
    {
        private readonly IUserService userService;

        public ListFriendsCommand(IUserService userService)
        {
            this.userService = userService;
        }

        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            var username = data[0];

            return this.userService.ListFriends(username);
        }
    }
}
