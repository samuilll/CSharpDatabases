using Forum.Data.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Services.Contracts
{
  public  interface IPostService
    {
        Post Create(string title,
            string content, int categoryId, int authorId);

        IQueryable<T> All<T>();

        Post ById(int postId);
    }
}
