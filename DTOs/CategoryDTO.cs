namespace ShoppingCartAPIs.DTOs
{
    public class CategoryDTO
    {
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public byte[]? CategoryImage { get; set; }
    }
}
