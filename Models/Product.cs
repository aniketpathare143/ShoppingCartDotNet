namespace ShoppingCartAPIs.Models
{
    public class Product
    {
        public Guid ProductId { get; set; } = Guid.NewGuid();
        public string? ProductName { get; set; }
        public int AvailableQuantity { get; set; }
        public byte[]? ProductImage { get; set; }
        public double Price { get; set; }

        // Navigation property for the related Category
        // Foreign key property
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<UserReview>? UserReviews { get; set; }

    }
}
