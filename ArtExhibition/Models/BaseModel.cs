namespace ArtExhibition.Models;

public abstract class BaseModel
{
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
}
