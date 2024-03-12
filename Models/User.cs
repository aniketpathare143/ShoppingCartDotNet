namespace ShoppingCartAPIs.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime DOB { get; set; }
        public string? Address { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
