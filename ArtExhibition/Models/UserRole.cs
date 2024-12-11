namespace ArtExhibition.Models
{
    public class UserRole
    {
        public string Username { get; set; } = string.Empty;
        public string Role { get; set;} = string.Empty;
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedDate { get; set; }
    }
}
