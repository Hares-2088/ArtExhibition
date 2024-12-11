using ArtExhibition.Models;

public class ExhibitionArtist
{
    public int ExhibitionId { get; set; }
    public Exhibition Exhibition { get; set; }

    public int ArtistId { get; set; }
    public Artist Artist { get; set; }
}