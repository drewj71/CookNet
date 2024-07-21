using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CookNet.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? DisplayName { get; set; }
        public string? ProfilePictureBase64 { get; set; }

        [NotMapped]
        public IBrowserFile ProfilePictureFile { get; set; }

        public string? Location { get; set; }
        public string? Biography { get; set; }
        public string? SocialMediaLinks { get; set; }
        public DateTime RegistrationDate { get; set; }
        public ICollection<UserCookbook> UserCookbooks { get; set; }

        public ApplicationUser() { 
            UserCookbooks = new List<UserCookbook>();
        }    
    }
}
