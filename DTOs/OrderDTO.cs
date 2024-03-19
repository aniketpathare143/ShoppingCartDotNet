using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DTOs
{
    public class OrderDTO
    {
        public Guid? OrderId { get; set; }
        public Guid UserId { get; set; }
        public int ProductPlacedCount { get; set; } = 0;
        public DateTime PlacedAt { get; set; } = DateTime.Now;        
        public List<PlacedOrderDTO>? PlacedOrders { get; set; }
        
    }
}
