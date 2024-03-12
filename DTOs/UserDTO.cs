using System.ComponentModel.DataAnnotations;

namespace ShoppingCartAPIs.DTOs
{
    public class UserDTO
    {
        public Guid? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; } 
        public string? Address { get; set; }
    }
}
