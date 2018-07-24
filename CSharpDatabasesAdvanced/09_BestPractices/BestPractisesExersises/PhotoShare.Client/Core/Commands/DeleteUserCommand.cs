namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;

    public class DeleteUserCommand:ICommand
    {
        private readonly IUserService userService;

        private const string successMessage = "User {0} was deleted from the database!";

        public DeleteUserCommand(IUserService userService)
        {
            this.userService = userService;
        }
        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!Session.HasCredentials(username))
            {
                throw new InvalidOperationExeption(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            this.userService.Delete(username);

                return string.Format(successMessage,username);
        }
    }
}
