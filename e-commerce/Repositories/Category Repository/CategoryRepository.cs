using e_commerce.Data;
using e_commerce.DTOs;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories.Category_Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) { _context = context; }
        public async Task Add(CategoryDTOPost dto)
        {
            Category category = new Category() 
            {
                Name = dto.Name,
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            if (dto.Name != null && dto.Products.Any())
            {
                var validProducts = await _context.Products.Where(x => dto.Products.Contains(x.Name)).ToListAsync();
                if (validProducts.Any() && validProducts != null) 
                { 
                    category.Products = validProducts;
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if(category == null)
                return false;
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CategoryDTOGet>> GetAll()
        {
            return await _context.Categories.
                Include(x => x.Products).ThenInclude(x => x.Users).
                Select(x => new CategoryDTOGet()
                { 
                    Name = x.Name,
                    Products = x.Products.Select(x => new ProductDTOGet 
                    {
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description,
                        Category = x.Category.Name,
                        Users = x.Users.Select(x => new UserDTOGet
                        {
                            Username = x.Username,
                            Email = x.Email,
                            Password = x.Password,
                        }).ToList()
                    }).ToList(),
                }).ToListAsync();
        }

        public async Task<CategoryDTOGet> GetById(int id)
        {
            var category = await _context.Categories.
                Where(x => x.Id == id).
                Include(x => x.Products).
                ThenInclude(x => x.Users).
                Select(x => new CategoryDTOGet()
                {
                    Name = x.Name,
                    Products = x.Products.Select(x => new ProductDTOGet
                    {
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description,
                        Category = x.Category.Name,
                        Users = x.Users.Select(x => new UserDTOGet
                        {
                            Username = x.Username,
                            Email = x.Email,
                            Password = x.Password,
                        }).ToList()
                    }).ToList(),
                }).FirstOrDefaultAsync();
            if (category == null)
                return null;
            return category;
        }

        public async Task<bool> Update(CategoryDTOPost dto, int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;
            category.Name = dto.Name;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            if (dto.Products != null && dto.Products.Any())
            {
                var validProducts = await _context.Products.Where(x => dto.Products.Contains(x.Name)).ToListAsync();
                if (validProducts.Any() && validProducts != null) 
                {
                    category.Products = validProducts;
                }
            }
            return true;
        }
    }
}
