using e_commerce.Models;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.DTOs
{
    public class CategoryDTOGet
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = string.Empty;
        public ICollection<ProductDTOGet>? Products { get; set; }
    }
    public class CategoryDTOPost
    {
        [MaxLength(100), Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<string>? Products { get; set; }
    }
}
