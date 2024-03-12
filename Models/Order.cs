namespace ShoppingCartAPIs.Models
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public int PlacedQuantity { get; set; }
        public DateTime PlacedAt { get; set; }

        // Navigation property for the related Product
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        // Navigation property for the user who placed the order (assuming it's a User entity)
        public Guid UserId { get; set; }
        public User? User { get; set; }

    }
}
