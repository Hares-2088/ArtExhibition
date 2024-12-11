using Microsoft.AspNetCore.Mvc;
using ArtExhibition.Data;
using ArtExhibition.Models;
using System.Security.Claims;

namespace ArtExhibition.Controllers
{
    public class CartController : Controller
    {
        private readonly GalleryDbContext _context;

        public CartController(GalleryDbContext context)
        {
            _context = context;
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user's ID
            if (userId == null) return RedirectToAction("Login", "Account");

            var cart = _context.Carts
                .Where(c => c.UserId == userId)
                .Select(c => new Cart
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    CartItems = c.CartItems.Select(ci => new CartItem
                    {
                        Id = ci.Id,
                        ArtworkId = ci.ArtworkId,
                        Artwork = new Artwork
                        {
                            Title = ci.Artwork.Title,
                            ImageUrl = ci.Artwork.ImageUrl
                        },
                        Quantity = ci.Quantity,
                        Price = ci.Price
                    }).ToList(),
                })
                .FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart
                {
                    CartItems = new List<CartItem>()
                };
            }

            return View(cart);
        }


        // POST: /Cart/AddItem
        [HttpGet]
        [HttpPost]
        public IActionResult AddItem(int artworkId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            var cart = _context.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                // Create a new cart for the user if it doesn't exist
                cart = new Cart { UserId = userId };
                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            var artwork = _context.Artworks.FirstOrDefault(a => a.ArtworkId == artworkId);
            if (artwork == null) return NotFound();

            // Check if the item is already in the cart
            var cartItem = _context.CartItems.FirstOrDefault(ci => ci.CartId == cart.Id && ci.ArtworkId == artworkId);
            if (cartItem != null)
            {
                cartItem.Quantity++; // Increment quantity if the item exists
            }
            else
            {
                // Add new item to the cart
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ArtworkId = artwork.ArtworkId,
                    Quantity = 1,
                    Price = artwork.Price
                };
                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // POST: /Cart/RemoveItem/{id}
        [HttpPost]
        public IActionResult RemoveItem(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized(); // Ensure the user is logged in
            }

            // Find the cart item by its ID and ensure it belongs to the current user's cart
            var cartItem = _context.CartItems
                .FirstOrDefault(ci => ci.Id == id && ci.Cart.UserId == userId);
    
            if (cartItem == null)
            {
                return NotFound(); // Cart item doesn't exist or doesn't belong to the user
            }

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
