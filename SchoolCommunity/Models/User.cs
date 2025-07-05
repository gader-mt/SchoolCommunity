using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolCommunity.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }  
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }    

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } 

        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}

