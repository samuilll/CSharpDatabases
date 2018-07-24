namespace PhotoShare.Client.Core.Commands
{
    using PhotoShare.Client.Core.Commands.Contracts;
    using PhotoShare.Services;
    using PhotoShare.Services.Contracts;

    public class RegisterUserCommand:ICommand
    {
        private readonly IUserService userService;

        public RegisterUserCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(string[] data)
        {
            if (Session.HasLoggedUser())
            {
                throw new InvalidOperationExeption(ExeptionMessageHandler.InvalidCredentialsExeption);
            }

            var user = this.userService.Create(data);

            return "User " + user.Username + " was registered successfully!";
        }
    }
}
