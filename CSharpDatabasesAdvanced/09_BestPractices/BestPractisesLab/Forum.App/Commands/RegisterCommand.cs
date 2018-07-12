using Forum.App.Commands.Contracts;
using Forum.Services;
using Forum.Services.Contracts;

namespace Forum.App.Commands
{
    public class RegisterCommand : ICommand
    {
        private readonly IUserService userService;

        public RegisterCommand(IUserService userService)
        {
            this.userService = userService;
        }

        public string Execute(params string[] arguments)
        {
            var username = arguments[0];
            var password = arguments[1];

           var user =  userService.Create(username, password);

            if (user==null)
            {
               return  "There is already an registered  user with that username!";
            }

            return "User created successfully";
        }
    }
}
