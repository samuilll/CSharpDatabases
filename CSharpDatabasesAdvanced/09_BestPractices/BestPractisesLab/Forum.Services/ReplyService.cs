using Forum.Data;
using Forum.Data.Models;
using Forum.Services.Contracts;
using AutoMapper;

namespace Forum.Services
{
    public class ReplyService : IReplyService
    {
        private readonly ForumDbContext context;

        public ReplyService(ForumDbContext context)
        {
            this.context = context;
        }
        public TModel Create<TModel>( int postId, int authorId, string replyText)
        {
            var reply = new Reply
            {
                Content = replyText,
                PostId = postId,
                AuthorId = authorId
            };

            context.Replies.Add(reply);

            context.SaveChanges();

            return Mapper.Map<TModel>(reply);
        }

        public void Delete(int replyId)
        {
            throw new System.NotImplementedException();
        }
    }
}
