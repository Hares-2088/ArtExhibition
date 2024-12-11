using System.ComponentModel.DataAnnotations;

namespace ArtExhibition.Models
{
    public class Artwork
    {
        public int ArtworkId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string ImageUrl { get; set; }

        // Optional: Description for additional details about the artwork
        public string? Description { get; set; }

        [Required(ErrorMessage = "A room must be selected.")]
        public int RoomId { get; set; }
        public Room Room { get; set; } // Navigation property

        // Foreign key to the Artist model
        public int ArtistId { get; set; }
        public Artist Artist { get; set; } // Navigation property
        
        public ICollection<ExhibitionArtwork> ExhibitionArtworks { get; set; } = new List<ExhibitionArtwork>();

    }
}