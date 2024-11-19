using System.ComponentModel.DataAnnotations;

namespace e_commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; } = string.Empty;

        //relation
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
