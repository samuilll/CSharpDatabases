

using Forum.App.Commands.Contracts;
using Forum.App.Models;
using Forum.Services.Contracts;
using System.Linq;

namespace Forum.App.Commands
{
    public class ReplyCommand : ICommand
    {
        private readonly IReplyService replyService;

        public ReplyCommand(IReplyService replyService)
        {
            this.replyService = replyService;
        }

        public string Execute(params string[] arguments)
        {
            var postId = int.Parse(arguments[0]);
            var content =string.Join(" ", arguments.Skip(1).ToArray());


            if (Session.User == null)
            {
                return "You are not logged in";
            }
            var authorId = Session.User.Id;

            replyService.Create<ReplyDto>(postId, authorId, content);

            return "Reply created successfully";
        }
    }
}
