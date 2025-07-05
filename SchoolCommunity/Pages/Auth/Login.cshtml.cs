using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolCommunity.Data;
using System.Linq;

namespace SchoolCommunity.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;
        public LoginModel(AppDbContext context) => _context = context;

        [BindProperty]
        public string InputEmail { get; set; } = string.Empty;

        [BindProperty]
        public string InputPassword { get; set; } = string.Empty;

        public string? Message { get; set; }

        public IActionResult OnPost()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == InputEmail && u.Password == InputPassword);

            if (user == null)
            {
                Message = "Invalid email or password.";
                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToPage("/User/Profile");
        }
    }
}
