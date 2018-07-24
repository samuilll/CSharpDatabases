using System.Collections.Generic;
using System.Linq;
using Forum.Data;
using Forum.Data.Models;
using Forum.Services.Contracts;
using AutoMapper.QueryableExtensions;

namespace Forum.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext context;

        public PostService(ForumDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> All<T>()
        {
            var posts = context.Posts
                .ProjectTo<T>();

            return posts;
        }


        public Post ById(int postId)
        {
            var post = context.Posts.Find(postId);

            return post;
        }

        public Post Create(string title, string content, int categoryId, int authorId)
        {
            var post =   new Post
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                AuthorId = authorId
            };

            context.Posts.Add(post);

            context.SaveChanges();

            return post;
        }

    }
}
