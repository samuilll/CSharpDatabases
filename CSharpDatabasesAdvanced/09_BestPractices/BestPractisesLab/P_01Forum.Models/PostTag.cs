
using System.ComponentModel.DataAnnotations;

namespace Forum.Data.Models
{
  public  class PostTag
    {
        public PostTag()
        {

        }
        public PostTag(int postId, int tagId)
        {
            PostId = postId;
            TagId = tagId;
        }

        public int PostId { get; set; }

        public virtual Post Post { get; set; }
        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
