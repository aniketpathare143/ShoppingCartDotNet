namespace ShoppingCartAPIs.DTOs
{
    public class RetrievePlacedOrderDTO
    {
        public Guid PlacedOrderId { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductName { get; set; }
        public int PlacedQuantity { get; set; }
    }
}
