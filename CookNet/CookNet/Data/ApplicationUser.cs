using Microsoft.AspNetCore.Identity;

namespace CookNet.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string FullName { get; set; }
        public string ProfilePictureURL { get; set; }
        public string Location { get; set; }
        public string Biography { get; set; }
        public string SocialMediaLinks { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
