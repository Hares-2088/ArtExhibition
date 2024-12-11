using ArtExhibition.Data;
using ArtExhibition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArtExhibition.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly GalleryDbContext _context;

        public UserController(GalleryDbContext context)
        {
            _context = context;
        }

        // GET: ArtistRequest
        public IActionResult ArtistRequest()
        {
            return View();
        }

        // POST: ArtistRequest
        [HttpPost]
        public IActionResult ArtistRequest(ArtistRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Re-display the form if validation fails
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            // Save the request
            var request = new ArtistRequest
            {
                UserId = userId,
                Reason = model.Reason,
                Status = "Pending" // Default status
            };

            _context.ArtistRequests.Add(request);
            _context.SaveChanges();

            // Redirect to the Request Submitted page
            return RedirectToAction("RequestSubmitted");
        }
        
        public IActionResult RequestSubmitted()
        {
            return View();
        }

    }
}