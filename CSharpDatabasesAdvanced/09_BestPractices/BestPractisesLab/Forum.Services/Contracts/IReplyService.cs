using Forum.Data.Models;

namespace Forum.Services.Contracts
{
 public   interface IReplyService
    {
        TModel Create<TModel>(int postId,int authorId, string replyText);

        void Delete(int replyId);
    }
}
