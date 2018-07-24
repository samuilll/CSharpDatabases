using Forum.App.Commands.Contracts;
using Forum.Services.Contracts;
using System.Linq;
using System.Text;
using AutoMapper.QueryableExtensions;
using Forum.App.Models;

namespace Forum.App.Commands
{
    public class ListPostsCommand : ICommand
    {
        private IPostService postService;

        public ListPostsCommand(IPostService postService)
        {
            this.postService = postService;
        }

        public string Execute(params string[] arguments)
        {
            var posts = postService.All<PostDto>()
                .GroupBy(p => p.CategoryName)
                .ToList();

            var sb = new StringBuilder();

            foreach (var group in posts)
            {
                sb.AppendLine(group.Key);

                foreach (var post in group)
                {
                    sb.AppendLine($" --{post.Id} {post.Title} - {post.Content} by {post.AuthorName}");

                }

            }

            return sb.ToString().TrimEnd('\r', '\n');
        }
    }
}
