using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPIs.Models
{
    public class UserReview
    {
        [Key]
        public Guid UserReviewId { get; set; } = Guid.NewGuid();
        public string ReviewMessage { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
        public Guid? UserId { get; set; }       
        public Product? Product { get; set; }
    }
}
