using System;
using System.Collections.Generic;

namespace ArtExhibition.Models
{
    public class Exhibition
    {
        public int ExhibitionId { get; set; } // Primary Key

        // Exhibition Details
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Room Association
        public int RoomId { get; set; } // Foreign Key
        public Room Room { get; set; } // Navigation Property

        // Relationships
        public ICollection<ExhibitionArtist> ExhibitionArtists { get; set; } = new List<ExhibitionArtist>();
        public ICollection<ExhibitionArtwork> ExhibitionArtworks { get; set; } = new List<ExhibitionArtwork>();
    }
}