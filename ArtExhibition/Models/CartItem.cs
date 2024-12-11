namespace ArtExhibition.Models
{
    public class CartItem
    {
        public int Id { get; set; } // Primary Key

        // Foreign Key to link the artwork being added
        public int ArtworkId { get; set; }
        public Artwork Artwork { get; set; } // Navigation property

        // Quantity of this item in the cart
        public int Quantity { get; set; }

        // Snapshot of the artwork price when it was added
        public decimal Price { get; set; }

        // Computed property for the subtotal of this item
        public decimal Subtotal => Price * Quantity;

        // Foreign Key to link the cart it belongs to
        public int CartId { get; set; }
        public Cart Cart { get; set; } // Navigation property
    }
}