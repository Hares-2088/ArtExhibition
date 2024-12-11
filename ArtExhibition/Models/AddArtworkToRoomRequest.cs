using System.ComponentModel.DataAnnotations;

namespace ArtExhibition.Models
{
    public class AddArtworkToRoomRequest
    {
        [Required]
        public int RoomId { get; set; }

        [Required]
        public int ArtworkId { get; set; }

    }
}
