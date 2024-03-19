namespace ShoppingCartAPIs.DTOs
{
    public class UserGetOrderDTO
    {
        public Guid UserId { get; set; }
        public string? UserName { get; set; }

        public List<RetrieveOrderDTO> RetrieveOrders { get; set; }
    }
}
