using System.ComponentModel.DataAnnotations;
using DinoForum.Data;

namespace DinoForum.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "A comment body is required.")]
        [Display(Name = "Message")]
        public string Content { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.Now;

        //Foreign Key
        public int DiscussionId { get; set; }

        public Discussion? Discussion { get; set; }

        public string DinoForumUserId { get; set; } = String.Empty;
        public DinoForumUser? DinoForumUser { get; set; }
    }
}
