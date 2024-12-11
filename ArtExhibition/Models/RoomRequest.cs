using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtExhibition.Models
{
    public class RoomRequest
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public int RoomId { get; set; } // Foreign Key

        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; } // Navigation Property for Room

        public int ArtistId { get; set; }// Match the type with Artist's primary key

        [ForeignKey(nameof(ArtistId))]
        public Artist? Artist { get; set; } // Navigation Property for Artist/User

        [Required]
        public DateTime StartDate { get; set; } // Start date of the exhibition

        [Required]
        public DateTime EndDate { get; set; } // End date of the exhibition

        [Required]
        public string Status { get; set; } = "Pending"; // Status of the request: Pending, Approved, Rejected

        public string? AdminRemarks { get; set; } // Optional remarks from the admin
    }
}