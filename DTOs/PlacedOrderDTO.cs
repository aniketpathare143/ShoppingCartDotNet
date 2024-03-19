namespace ShoppingCartAPIs.DTOs
{
    public class PlacedOrderDTO
    {
        public Guid? PlacedOrderId { get; set; }
        public int PlacedQuantity { get; set; } = 0;
        public Guid CategoryId { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
    }
}
