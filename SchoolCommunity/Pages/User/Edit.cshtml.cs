using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolCommunity.Data;
using SchoolCommunity.Models;
using System.Linq;

namespace SchoolCommunity.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        public EditModel(AppDbContext context) => _context = context;

        [BindProperty]
        public SchoolCommunity.Models.User? InputUser { get; set; }

        [BindProperty]
        public string? NewPassword { get; set; }

        public string? Message { get; set; }

        public IActionResult OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Auth/Login");

            var user = _context.Users.Find(userId);
            if (user == null)
                return RedirectToPage("/Auth/Login");

            InputUser = new SchoolCommunity.Models.User
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Bio = user.Bio,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Auth/Login");

            if (InputUser == null)
            {
                Message = "Invalid form data.";
                return Page(); 
            }

            var existingUser = _context.Users.Find(userId);
            if (existingUser == null)
                return RedirectToPage("/Auth/Login");

            existingUser.Username = InputUser.Username ?? existingUser.Username;
            existingUser.Email = InputUser.Email ?? existingUser.Email;
            existingUser.Bio = InputUser.Bio;
            existingUser.ProfilePictureUrl = InputUser.ProfilePictureUrl;

            if (!string.IsNullOrWhiteSpace(NewPassword))
                existingUser.Password = NewPassword;

            _context.SaveChanges();
            Message = "Profile updated successfully!";
            return RedirectToPage("/User/Profile");
        }
    }
}
