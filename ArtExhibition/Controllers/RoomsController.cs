using ArtExhibition.Data;
using ArtExhibition.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using ArtExhibition.Models.ViewModels;

namespace ArtExhibition.Controllers;

public class RoomsController : Controller
{
    private readonly GalleryDbContext _context;

    public RoomsController(GalleryDbContext context)
    {
        _context = context;
    }

    // GET: Rooms/ScheduleRequest
    public IActionResult ScheduleRequest()
    {
        var rooms = _context.Rooms?.Select(r => new SelectListItem
        {
            Value = r.RoomId.ToString(),
            Text = r.Name
        }).ToList() ?? new List<SelectListItem>();

        var viewModel = new ScheduleRequestViewModel
        {
            Rooms = rooms
        };

        return View(viewModel);
    }

    // POST: Rooms/ScheduleRequest
    [HttpPost]
    public IActionResult SubmitRequest(ScheduleRequestViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Rooms = _context.Rooms?.Select(r => new SelectListItem
            {
                Value = r.RoomId.ToString(),
                Text = r.Name
            }).ToList() ?? new List<SelectListItem>();

            return View("ScheduleRequest", model);
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

        // Convert userId to int
        if (!int.TryParse(userId, out int artistId))
        {
            ModelState.AddModelError("", "Invalid user ID.");
            model.Rooms = _context.Rooms?.Select(r => new SelectListItem
            {
                Value = r.RoomId.ToString(),
                Text = r.Name
            }).ToList() ?? new List<SelectListItem>();
            return View("ScheduleRequest", model);
        }

        var roomRequest = new RoomRequest
        {
            RoomId = model.RoomId,
            ArtistId = artistId, // Use the converted artistId
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Status = "Pending"
        };

        _context.RoomRequests.Add(roomRequest); // Use correct DbSet name
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

}