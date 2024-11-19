using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]    
        public string Name { get; set; } = string.Empty;
        [Range(0.1,10000)]
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        //Relations
        public ICollection<User> Users { get; set; } = new List<User>();

        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
    }
}
