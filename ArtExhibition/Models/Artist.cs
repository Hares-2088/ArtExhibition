using System;
using System.Collections.Generic;

namespace ArtExhibition.Models
{
    public class Artist
    {
        public int ArtistId { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Nationality { get; set; }
        public string Genre { get; set; }
        public string? ProfilePictureUrl { get; set; }

        // Navigation Properties
        public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
        public ICollection<ExhibitionArtist> ExhibitionArtists { get; set; } = new List<ExhibitionArtist>();
    }
}