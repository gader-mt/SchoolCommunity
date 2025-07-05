using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolCommunity.Data;
using System.Collections.Generic;
using System.Linq;

namespace SchoolCommunity.Pages
{
    public class SearchModel : PageModel
    {
        private readonly AppDbContext _context;
        public SearchModel(AppDbContext context) => _context = context;

        [BindProperty(SupportsGet = true)]
        public string? Query { get; set; }

        public List<SchoolCommunity.Models.Post> PostResults { get; set; } = new();
        public List<SchoolCommunity.Models.User> UserResults { get; set; } = new();

        public void OnGet()
        {
            var keyword = Query?.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
                return;

            PostResults = _context.Posts
       .Include(p => p.User)
       .Where(p =>
           (p.Title ?? string.Empty).Contains(keyword) ||
           (p.Content ?? string.Empty).Contains(keyword)
       )
       .ToList();


            UserResults = _context.Users
                .Where(u =>
                    (u.Username ?? string.Empty).Contains(keyword) ||
                    (u.Email ?? string.Empty).Contains(keyword)
                )
                .ToList();
        }
    }
}
