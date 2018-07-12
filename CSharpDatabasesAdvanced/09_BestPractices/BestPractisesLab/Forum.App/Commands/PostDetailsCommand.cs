using Forum.App.Commands.Contracts;
using Forum.Data.Models;
using Forum.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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

            sb.AppendLine($"{post.Title} by {post.Author.Username}");

            foreach (var reply in post.Replies)
            {
                sb.AppendLine($"-{reply.Content} by {reply.Author.Username}");
            }
            return sb.ToString().TrimEnd('\n','\r');
        }
    }
}
