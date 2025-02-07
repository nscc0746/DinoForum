using System.ComponentModel.DataAnnotations;

namespace DinoForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "A comment body is required.")]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        //Foreign Key
        public int DiscussionId { get; set; }
    }
}
