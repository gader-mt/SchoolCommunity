using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolCommunity.Data;
using SchoolCommunity.Models;
using System.Linq;

namespace SchoolCommunity.Pages.User
{
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _context;
        public ProfileModel(AppDbContext context) => _context = context;

        public SchoolCommunity.Models.User? CurrentUser { get; set; }


        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Auth/Login");
            CurrentUser = _context.Users
                .Include(u => u.Posts)
                .FirstOrDefault(u => u.Id == userId);
            return Page();
        }

        public IActionResult OnPostDeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
