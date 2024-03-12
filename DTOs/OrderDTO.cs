using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DTOs
{
    public class OrderDTO
    {
        public int PlacedQuantity { get; set; }
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

        // Navigation property for the related Product
        public Guid ProductId { get; set; }

        // Navigation property for the user who placed the order (assuming it's a User entity)
        public Guid UserId { get; set; }
    }
}
