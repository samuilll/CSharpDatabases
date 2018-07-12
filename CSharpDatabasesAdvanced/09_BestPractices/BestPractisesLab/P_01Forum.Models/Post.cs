using System.Collections.Generic;

namespace Forum.Data.Models
{
    public class Post
    {
        public Post()
        {
        }

        public Post(string title,string content, Category category, User user)
        {
            this.Content = content;
            this.Category = category;
            this.Author = user;
            this.Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<PostTag> PostsTags { get; set; }
    }
}