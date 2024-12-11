using Microsoft.AspNetCore.Identity;

namespace ArtExhibition.Models
{
    public class User : IdentityUser
    {
        // Optional profile information
        public string? FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Address { get; set; } 

        // Navigation properties
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>(); // For Artists

        // Timestamps for auditing
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}