namespace ShoppingCartAPIs.Models
{
    public class Order
    {
        public Guid? OrderId { get; set; } = Guid.NewGuid();
        public DateTime PlacedAt { get; set; } = DateTime.Now;
        public int ProductPlacedCount { get; set; }
        public double PriceTotal { get; set; }
        // Navigation property for the user who placed the order 
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public List<PlacedOrder>? PlacedOrders { get; set; }

    }
}
