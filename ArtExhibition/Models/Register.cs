namespace ArtExhibition.Models
{
    public class Register
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        
        public string FullName { get; set; }
        public string Role { get; set; }
        public string? ProfilePictureUrl { get; set; }

    }
}
