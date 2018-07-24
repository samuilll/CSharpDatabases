namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Data;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;
    using System;
    using System.Linq;

    public class ModifyUserCommand : ICommand
    {
        private readonly IUserService userService;

        public ModifyUserCommand(IUserService userService, ITownService townService)
        {
            this.userService = userService;
        }

        public string Execute(string[] data)
        {
            using (var context = new PhotoShareContext())
            {
                var username = data[0];
                var propertyName = data[1];
                string newValueAsString = data[2];

                bool hasCredentials = Session.HasCredentials(username);

                if (!hasCredentials)
                {
                    throw new InvalidOperationException(ExeptionMessageHandler.InvalidCredentialsExeption);
                }


                var user =   this.userService.Modify(data);

             var propertyValue = user.GetType().GetProperties().First(pi => pi.Name == propertyName).GetValue(user);

                return $"User {username} {propertyName} is {propertyValue}.";
            }

        }

    }
}
