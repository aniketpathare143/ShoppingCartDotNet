namespace ShoppingCartAPIs.Models
{
    public class Category
    {
        public Guid CategoryId { get; set; } = Guid.NewGuid();
        public string? CategoryName { get; set; }
        public byte[]? CategoryImage { get; set; }
        public List<Product>? Products { get; set; }
    }
}
