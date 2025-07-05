﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolCommunity.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }  

        [Required, EmailAddress]
        public string? Email { get; set; }     

        [Required]
        public string? Password { get; set; } 

        public string? Bio { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}
