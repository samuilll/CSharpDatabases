using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;

namespace Forum.App.Commands
{
    public class LoginCommand : ICommand
    {

        private IUserService userService;

        public LoginCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] arguments)
        {
            var username = arguments[0];
            var password = arguments[1];

            var User = userService.ByUsernameAndPassword(username, password);

            if (User==null)
            {
                return "Invalid username or password";
            }

            Session.User = User;

            return $"Logged in successfully";
        }
    }
}
