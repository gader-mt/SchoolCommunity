using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolCommunity.Data;
using SchoolCommunity.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolCommunity.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context) => _context = context;

        public List<SchoolCommunity.Models.Post>? RecentPosts { get; set; }



        public void OnGet()
        {
            RecentPosts = _context.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreatedAt)
                .Take(20)
                .ToList();
        }
    }
}
