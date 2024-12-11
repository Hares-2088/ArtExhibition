using System.ComponentModel.DataAnnotations;

namespace ArtExhibition.Models
{
    public class ArtistRequest
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign Key to User
        public User User { get; set; } // Navigation Property

        [Required(ErrorMessage = "Please provide a reason for your request.")]
        public string Reason { get; set; }

        public string Status { get; set; } = "Pending";
        public string? AdminRemarks { get; set; }
    }
}