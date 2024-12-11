using ArtExhibition.Data;
using ArtExhibition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ExhibitionController : Controller
{
    private readonly GalleryDbContext _context;

    public ExhibitionController(GalleryDbContext context)
    {
        _context = context;
    }

    [Route("Exhibitions")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        var exhibitions = _context.Exhibitions
            .Include(e => e.Room)
            .Include(e => e.ExhibitionArtists)
            .ThenInclude(ea => ea.Artist)
            .ToList();
        return View(exhibitions); 
    }

    [AllowAnonymous]
    public IActionResult Details(int id)
    {
        var exhibition = _context.Exhibitions
            .Include(e => e.Room) // Include Room details
            .Include(e => e.ExhibitionArtists)
            .ThenInclude(ea => ea.Artist) // Include Artists
            .Include(e => e.ExhibitionArtworks)
            .ThenInclude(ea => ea.Artwork) // Include Artworks
            .FirstOrDefault(e => e.ExhibitionId == id);

        if (exhibition == null)
        {
            return NotFound();
        }

        return View(exhibition);
    }


    // Keep authorization for actions like Create, Edit, and Delete
    [Authorize(Roles = "Admin,Artist")]
    public IActionResult Create()
    {
        ViewData["Rooms"] = _context.Rooms.Select(r => new { r.RoomId, r.Name }).ToList();
        ViewData["Artists"] = _context.Artists.Select(a => new { a.ArtistId, a.Name }).ToList();
        return View();
    }

    [Authorize(Roles = "Admin,Artist")]
    [HttpPost]
    public IActionResult Create(Exhibition model, int[] artistIds)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Rooms"] = _context.Rooms.Select(r => new { r.RoomId, r.Name }).ToList();
            ViewData["Artists"] = _context.Artists.Select(a => new { a.ArtistId, a.Name }).ToList();
            return View(model);
        }

        _context.Exhibitions.Add(model);
        _context.SaveChanges();

        foreach (var artistId in artistIds)
        {
            _context.ExhibitionArtists.Add(new ExhibitionArtist
            {
                ExhibitionId = model.ExhibitionId,
                ArtistId = artistId
            });
        }

        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin,Artist")]
    public IActionResult Edit(int id)
    {
        var exhibition = _context.Exhibitions.Find(id);
        if (exhibition == null) return NotFound();

        ViewData["Rooms"] = _context.Rooms.Select(r => new { r.RoomId, r.Name }).ToList();
        ViewData["Artists"] = _context.Artists.Select(a => new { a.ArtistId, a.Name }).ToList();
        return View(exhibition);
    }

    [Authorize(Roles = "Admin,Artist")]
    [HttpPost]
    public IActionResult Edit(Exhibition model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Rooms"] = _context.Rooms.Select(r => new { r.RoomId, r.Name }).ToList();
            ViewData["Artists"] = _context.Artists.Select(a => new { a.ArtistId, a.Name }).ToList();
            return View(model);
        }

        _context.Exhibitions.Update(model);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [Authorize(Roles = "Admin,Artist")]
    public IActionResult Delete(int id)
    {
        var exhibition = _context.Exhibitions.Find(id);
        if (exhibition == null) return NotFound();

        return View(exhibition);
    }

    [Authorize(Roles = "Admin,Artist")]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var exhibition = _context.Exhibitions.Find(id);
        if (exhibition == null) return NotFound();

        _context.Exhibitions.Remove(exhibition);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
}
