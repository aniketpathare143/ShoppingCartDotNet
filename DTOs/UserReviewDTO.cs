using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DTOs
{
    public class UserReviewDTO
    {
        public Guid? ReviewId { get; set; }
        public string ReviewMessage { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Guid? UserId { get; set; }
    }
}
