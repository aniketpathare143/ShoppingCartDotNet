using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DTOs
{
    public class ProductDTO
    {
        public Guid? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int AvailableQuantity { get; set; }
        public byte[]? ProductImage { get; set; }
        public double Price { get; set; }

        // Navigation property for the related Category
        public Guid CategoryId { get; set; }

    }
}
