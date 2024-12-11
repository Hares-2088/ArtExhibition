using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ArtExhibition.Data;
using ArtExhibition.Models;
using ArtExhibition.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ArtExhibition.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly GalleryDbContext _context;

        public ArtistsController(GalleryDbContext context)
        {
            _context = context;
        }

        // GET: /Artists
        public IActionResult Index()
        {
            var artists = _context.Artists.ToList();
            return View(artists);
        }

        // GET: /Artists/Details/{id}
        public IActionResult Details(int id)
        {
            var artist = _context.Artists
                .Include(a => a.Artworks) // Eagerly load related artworks
                .FirstOrDefault(a => a.ArtistId == id);

            if (artist == null)
            {
                return NotFound();
            }

            return View(artist);
        }


        // GET: /Artists/Create
        public IActionResult Create() => View();

        // POST: /Artists/Create
        [HttpPost]
        public IActionResult Create(Artist model)
        {
            if (!ModelState.IsValid)
            {
                // Return the same view with validation errors
                return View(model);
            }

            // Add artist to the database
            _context.Artists.Add(model);
            _context.SaveChanges();

            // Redirect to the Index page after successful creation
            return RedirectToAction("Index");
        }


        // GET: /Artists/Edit/{id}
        public IActionResult Edit(int id)
        {
            var artist = _context.Artists.FirstOrDefault(a => a.ArtistId == id);
            if (artist == null) return NotFound();
            return View(artist);
        }

        // POST: /Artists/Edit/{id}
        [HttpPost]
        public IActionResult Edit(Artist model)
        {
            if (!ModelState.IsValid) return View(model);

            _context.Artists.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: /Artists/Delete/{id}
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var artist = _context.Artists.FirstOrDefault(a => a.ArtistId == id);
            if (artist == null) return NotFound();

            _context.Artists.Remove(artist);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
