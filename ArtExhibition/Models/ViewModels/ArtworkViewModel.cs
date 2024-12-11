using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ArtExhibition.Models.ViewModels
{
    public class ArtworkViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required]
        [Url(ErrorMessage = "Please provide a valid URL.")]
        public string ImageUrl { get; set; }

        [Required]
        public int RoomId { get; set; }

        public IEnumerable<SelectListItem> Rooms { get; set; }
    }
}