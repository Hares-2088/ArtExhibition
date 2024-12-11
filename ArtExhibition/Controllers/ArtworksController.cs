using ArtExhibition.Data; // Ensure this namespace is included
using ArtExhibition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtExhibition.Controllers
{
    public class ArtworksController : Controller
    {
        private readonly GalleryDbContext _context; // Add this field

        // Inject the database context via constructor
        public ArtworksController(GalleryDbContext context)
        {
            _context = context; // Initialize the field
        }

        // Public access: Anyone can view the list of artworks
        public IActionResult Index()
        {
            Console.WriteLine("ArtworksController.Index invoked");

            var artworks = _context.Artworks.ToList(); // Fetch artworks
            return View(artworks); // Pass artworks to the view
        }

        // Restricted access: Only Artists can add artworks
        [Authorize(Roles = "Artist")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public IActionResult Create(Artwork model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Artworks.Add(model); // Save artwork to the database
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Restricted access: Only Artists can edit artworks
        [Authorize(Roles = "Artist")]
        public IActionResult Edit(int id)
        {
            var artwork = _context.Artworks.FirstOrDefault(a => a.ArtworkId == id);
            if (artwork == null) return NotFound();

            return View(artwork);
        }

        [HttpPost]
        [Authorize(Roles = "Artist")]
        public IActionResult Edit(Artwork model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _context.Artworks.Update(model); // Update artwork in the database
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Details(int id)
        {
            var artwork = _context.Artworks
                .Include(a => a.Artist) // Include Artist data
                .FirstOrDefault(a => a.ArtworkId == id);
        
            if (artwork == null)
            {
                return NotFound(); // Return 404 if artwork is not found
            }
        
            return View(artwork);
        }

    }
}
