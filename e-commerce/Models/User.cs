using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Username { get; set; } = string.Empty; //it must be unique 
        [EmailAddress]
        public string Email = string.Empty;
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;
        
        //Relations
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public int PaymentCardId {  get; set; }
        [ForeignKey("PaymentCardId")]
        public PaymentCard? PaymentCard { get; set; }
    }
}
