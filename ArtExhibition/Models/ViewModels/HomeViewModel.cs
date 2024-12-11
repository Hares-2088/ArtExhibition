using System.Collections.Generic;

namespace ArtExhibition.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Artwork> FeaturedArtworks { get; set; } = new List<Artwork>();
        public IEnumerable<Artist> Artists { get; set; } = new List<Artist>();
    }
}