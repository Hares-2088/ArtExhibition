using ArtExhibition.Data;
using ArtExhibition.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtExhibition.Controllers;

public class AdminController : Controller
{
    private readonly GalleryDbContext _context;

    public AdminController(GalleryDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Requests
    public IActionResult Requests()
    {
        var roomRequests = _context.RoomRequests
            .Include(r => r.Room)
            .Include(r => r.Artist) // Ensure Artist data is included
            .Where(r => r.Status == "Pending")
            .ToList();

        return View(roomRequests);
    }


    // POST: Admin/Approve
    [HttpPost]
    public IActionResult Approve(int id)
    {
        var request = _context.RoomRequests.Find(id);
        if (request == null) return NotFound();

        request.Status = "Approved";
        _context.SaveChanges();

        return RedirectToAction("Requests");
    }

    // POST: Admin/Reject
    [HttpPost]
    public IActionResult Reject(int id, string? remarks)
    {
        var request = _context.RoomRequests.Find(id);
        if (request == null) return NotFound();

        request.Status = "Rejected";
        request.AdminRemarks = remarks;
        _context.SaveChanges();

        return RedirectToAction("Requests");
    }

    // GET: Admin/ArtistRequests
    public IActionResult ArtistRequests()
    {
        var artistRequests = _context.ArtistRequests
            .Include(r => r.User) // Include User navigation property
            .Where(r => r.Status == "Pending")
            .ToList();

        return View(artistRequests);
    }

    // POST: Admin/ApproveArtistRequest
    [HttpPost]
    public IActionResult ApproveArtistRequest(int id)
    {
        var request = _context.ArtistRequests
            .Include(r => r.User) // Include User navigation property
            .FirstOrDefault(r => r.Id == id);

        if (request == null) return NotFound();

        request.Status = "Approved";

        // Create a new Artist entry in the database
        var artist = new Artist
        {
            Name = request.User.FullName, // Use User's FullName
            Email = request.User.Email,
            ProfilePictureUrl = request.User.ProfilePictureUrl // Optional
        };

        _context.Artists.Add(artist);
        _context.SaveChanges();

        return RedirectToAction("ArtistRequests");
    }


    // POST: Admin/RejectArtistRequest
    [HttpPost]
    public IActionResult RejectArtistRequest(int id, string? remarks)
    {
        var request = _context.ArtistRequests.Find(id);
        if (request == null) return NotFound();

        request.Status = "Rejected";
        request.AdminRemarks = remarks;
        _context.SaveChanges();

        return RedirectToAction("ArtistRequests");
    }
    
    // POST: /Artworks/Delete/{id}
    [HttpPost]
    [Authorize(Roles = "Admin,Artist")]
    public IActionResult Delete(int id)
    {
        var artwork = _context.Artworks.Find(id);
        if (artwork == null)
        {
            return NotFound();
        }

        _context.Artworks.Remove(artwork);
        _context.SaveChanges();

        return RedirectToAction("Index"); // Redirect to the list of artworks
    }
}
