using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolCommunity.Data;
using SchoolCommunity.Models;
using System.Linq;

namespace SchoolCommunity.Pages.Post
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;
        public DetailsModel(AppDbContext context) => _context = context;

        public SchoolCommunity.Models.Post? Post { get; set; }  // ✅ nullable

        [BindProperty]
        public string? NewCommentText { get; set; }  // ✅ nullable

        public bool IsLoggedIn => HttpContext.Session.GetInt32("UserId") != null;

        public IActionResult OnGet(int id)
        {
            Post = _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefault(p => p.Id == id);

            if (Post == null)
                return RedirectToPage("/User/Profile");

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Auth/Login");

            var post = _context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            if (post == null || string.IsNullOrWhiteSpace(NewCommentText))
                return RedirectToPage(new { id });

            var comment = new Comment
            {
                Text = NewCommentText,
                UserId = userId.Value,
                PostId = id,
                CreatedAt = DateTime.Now
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return RedirectToPage(new { id });
        }
    }
}
