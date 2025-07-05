using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolCommunity.Models;
using SchoolCommunity.Data;
using System.Linq;

namespace SchoolCommunity.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;
        public RegisterModel(AppDbContext context) => _context = context;

        [BindProperty]
        public SchoolCommunity.Models.User? NewUser { get; set; }  

        public string? Message { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (NewUser == null)  
            {
                Message = "Something went wrong. Please try again.";
                return Page();
            }

            if (_context.Users.Any(u => u.Email == NewUser.Email))
            {
                Message = "Email already registered.";
                return Page();
            }

            _context.Users.Add(NewUser);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("UserId", NewUser.Id);
            return RedirectToPage("/User/Profile");
        }
    }
}
