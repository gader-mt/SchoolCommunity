using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SchoolCommunity.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}
