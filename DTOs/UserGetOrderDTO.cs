namespace ShoppingCartAPIs.DTOs
{
    public class UserGetOrderDTO
    {
        public string? Name { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductName { get; set; }
        public int PlacedQuantity { get; set; }
        public DateTime PlacedAt { get; set; }
    }
}
