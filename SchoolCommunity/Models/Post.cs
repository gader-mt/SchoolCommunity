using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolCommunity.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }  

        [Required]
        public string? Content { get; set; }  

        public string? ImageUrl { get; set; }
        public string? Link { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int UserId { get; set; }

        public User? User { get; set; }  

        public List<Comment> Comments { get; set; } = new();
    }
}

