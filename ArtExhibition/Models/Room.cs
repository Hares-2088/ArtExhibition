namespace ArtExhibition.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Floor { get; set; }
        public string Image { get; set; } = string.Empty;

        // Navigation property for related artworks
        public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
        
        public bool IsPublic { get; set; } = true;
        public int Capacity { get; set; }
    }
}
