using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DinoForum.Data;

namespace DinoForum.Models
{
    public class Discussion
    {
        //PK
        public int DiscussionId { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Post content is required.")]
        [Display(Name = "Message")]
        public string Content { get; set; } = string.Empty;

        //Images are optional, dates are auto generated
        public string ImageFilename { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //Handle image upload
        [NotMapped]
        [Display(Name = "Picture")]
        public IFormFile? ImageFile { get; set; }

        public List<Comment>? Comments { get; set; }

        public string DinoForumUserId { get; set; } = String.Empty;
        public DinoForumUser? DinoForumUser { get; set; }
    }
}
