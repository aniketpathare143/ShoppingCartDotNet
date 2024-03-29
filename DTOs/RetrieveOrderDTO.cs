namespace ShoppingCartAPIs.DTOs
{
    public class RetrieveOrderDTO
    {
        public Guid OrderId { get; set; }
        public DateTime PlacedAt { get; set; }
        public int ProductPlacedCount { get; set; }
        public double PriceTotal { get; set; }
        public List<RetrievePlacedOrderDTO> RetrievePlacedOrders { get; set; }
    }
}
