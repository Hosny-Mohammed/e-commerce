using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models
{
    public class PaymentCard
    {
        [Key]
        public int Id { get; set; }
        [Required, CreditCard]
        public string CardNumber { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string HolderName { get; set; } = string.Empty;
        [Range(typeof(DateTime), "1/1/2024", "12/31/2100", ErrorMessage = "Date must be between 1/1/2020 and 12/31/2025.")]
        public DateTime ExpireYear { get; set; }
        
        //Relation
        //public int UserId {  get; set; }
        //[ForeignKey("UserId")]
        //public User? User { get; set; }
    }
}
