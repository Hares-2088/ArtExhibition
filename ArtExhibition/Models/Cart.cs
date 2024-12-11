namespace ArtExhibition.Models
{
    public class Cart
    {
        public int Id { get; set; } // Primary Key

        // Foreign Key for User
        public string UserId { get; set; } // Change to string
        public User User { get; set; } // Navigation property

        // Collection of Cart Items
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

        // Computed Total
        public decimal Total => CartItems.Sum(item => item.Subtotal);
    }
}