using Microsoft.AspNetCore.Identity;

namespace DinoForum.Data
{
    public class DinoForumUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; } = String.Empty;

        [PersonalData]
        public string Location { get; set; } = String.Empty;

        [PersonalData]
        public string FavoriteDino { get; set; } = String.Empty;
    }
}
