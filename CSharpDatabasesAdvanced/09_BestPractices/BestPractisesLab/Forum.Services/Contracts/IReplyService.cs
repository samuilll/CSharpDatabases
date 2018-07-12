using Forum.Data.Models;

namespace Forum.Services.Contracts
{
 public   interface IReplyService
    {
        Reply Create(int postId,int authorId, string replyText);

        void Delete(int replyId);
    }
}
