using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolCommunity.Data;
using SchoolCommunity.Models;

namespace SchoolCommunity.Pages.Post
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public SchoolCommunity.Models.Post Post { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }  

        public string? Message { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
                return RedirectToPage("/Auth/Login");

            return Page();
        }

        public IActionResult OnPost()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("/Auth/Login");

            if (!ModelState.IsValid)
            {
                Message = "Invalid form data.";
                return Page();
            }

            Post.UserId = userId.Value;
            Post.CreatedAt = DateTime.Now;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                ImageFile.CopyTo(fileStream);

                Post.ImageUrl = "/uploads/" + uniqueFileName;
            }

            _context.Posts.Add(Post);
            _context.SaveChanges();

            return RedirectToPage("/User/Profile");
        }
    }
}
