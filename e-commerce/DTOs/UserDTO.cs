using e_commerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.DTOs
{
    public class UserDTOGet
    {
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty; //it must be unique 
        [EmailAddress]
        public string Email = string.Empty;
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        public ICollection<ProductDTOGet>? Products { get; set; }

        public PaymentCardDTOGet? PaymentCardDTOGet { get; set; }
    }
    public class UserDTOPost
    {
        [MaxLength(100), Required]
        public string Username { get; set; } = string.Empty; //it must be unique 
        [EmailAddress, Required]
        public string Email = string.Empty;
        [MinLength(6), Required]
        public string Password { get; set; } = string.Empty;
        public List<string>? Products { get; set; }
        public PaymentCardDTOPost? PaymentCardDTOPost { get; set; }
    }
}
