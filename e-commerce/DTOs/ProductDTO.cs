using e_commerce.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.DTOs
{
    public class ProductDTOGet
    {
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [Range(0.1, 10000)]
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        //Relations
        public ICollection<UserDTOGet>? Users { get; set; }

        public string Category { get; set; } = string.Empty;
    }
    public class ProductDTOPost
    {
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;
        [Range(0.1, 10000)]
        public decimal Price { get; set; }
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        //Relations
        public ICollection<string>? Users { get; set; }
        public string Category { get; set; } = string.Empty;
        //public int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        //public Category? Category { get; set; }
    }
}
