using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ArtExhibition.Data;
using ArtExhibition.Models;
using ArtExhibition.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ArtExhibition.Controllers
{
    public class HomeController : Controller
    {
        private readonly GalleryDbContext _context;

        public HomeController(GalleryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                FeaturedArtworks = _context.Artworks.Take(6).ToList(),
                Artists = _context.Artists.Take(3).ToList()
            };
            return View(model);
        }

    }
}