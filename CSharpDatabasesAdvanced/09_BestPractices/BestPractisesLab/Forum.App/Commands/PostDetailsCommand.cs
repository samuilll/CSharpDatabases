using System.Linq;
using Forum.App.Commands.Contracts;
using Forum.Data.Models;
using Forum.Services.Contracts;
using System.Text;
using AutoMapper;
using Forum.App.Models;

namespace Forum.App.Commands
{
   public class PostDetailsCommand:ICommand
    {
        private readonly IPostService postService;

        public PostDetailsCommand(IPostService postService)
        {
            this.postService = postService;
        }

        public string Execute(params string[] arguments)
        {
            var postId = int.Parse(arguments[0]);

            var sb = new StringBuilder();

            Post post = postService.ById(postId);

            var postDetailsDto = Mapper.Map<PostDetailsDto>(post);
          
            sb.AppendLine($"{postDetailsDto.Title} by {postDetailsDto.AuthorUserName}");

            foreach (var reply in postDetailsDto.Replies)
            {
                sb.AppendLine($"-{reply.Content} by {reply.AuthorUserName}");
            }
            return sb.ToString().TrimEnd('\n','\r');
        }
    }
}
