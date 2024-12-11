using ArtExhibition.Models;

public class ExhibitionArtwork
{
    public int ExhibitionId { get; set; }
    public int ArtworkId { get; set; }

    // Optional navigation properties
    public Exhibition? Exhibition { get; set; }
    public Artwork? Artwork { get; set; }
}
