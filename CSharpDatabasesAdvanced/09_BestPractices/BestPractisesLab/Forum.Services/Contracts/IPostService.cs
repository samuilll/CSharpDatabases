using Forum.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace Forum.Services.Contracts
{
  public  interface IPostService
    {
        Post Create(string title,
            string content, int categoryId, int authorId);

        IEnumerable<Post> All();


        Post ById(int postId);
    }
}
