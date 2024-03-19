namespace ShoppingCartAPIs.Models
{
    public class PlacedOrder
    {
        public Guid PlacedOrderId { get; set; } = Guid.NewGuid();
        public int PlacedQuantity { get; set; } = 0;

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
              
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
