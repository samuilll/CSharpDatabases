using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.App.Models
{
   public class PostDetailsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public  string Content  { get; set; }

        public string AuthorUserName { get; set; }

        public IEnumerable<ReplyDto> Replies { get; set; }

        public int ReplyCount { get; set; }
    }
}
