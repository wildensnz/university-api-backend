using Microsoft.Build.Framework;

namespace university_api_backend.Models.DataModels
{
    public class UserLogins
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
