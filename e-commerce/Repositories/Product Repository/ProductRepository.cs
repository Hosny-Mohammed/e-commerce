using e_commerce.Data;
using e_commerce.DTOs;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories.Product_Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) { _context = context; }
        public async Task Add(ProductDTOPost dto)
        {
            Product product = new Product() 
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
            };
            await _context.Products.AddAsync(product);
           // await _context.SaveChangesAsync();

            if (dto.Users != null && dto.Users.Any()) 
            {
                var validUsers = await _context.Users.Where(x => dto.Users.Contains(x.Username)).ToListAsync();

                if(validUsers.Any() && validUsers != null)
                {
                    product.Users = validUsers;
                    await _context.SaveChangesAsync();
                }
            }

            if (dto.Category != null && dto.Category != "") 
            {
                var validCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == dto.Category);
                if (validCategory != null) 
                { 
                    product.Category = validCategory;
                    product.CategoryId = validCategory.Id;
                    //await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
                return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDTOGet>> GetAll()
        {
            return await _context.Products.
                Include(x => x.Users).ThenInclude(x => x.PaymentCard).
                Include(x => x.Category).
                Select(x => new ProductDTOGet
                {
                    Name = x.Name,
                    Price = x.Price,
                    Category = x.Category.Name,
                    Users = x.Users.Select(x => new UserDTOGet
                    {
                        Username = x.Username,
                        Email = x.Email,
                        PaymentCardDTOGet = new PaymentCardDTOGet
                        {
                            CardNumber = x.PaymentCard.CardNumber,
                            HolderName = x.PaymentCard.HolderName,
                            ExpireYear = x.PaymentCard.ExpireYear,
                        }

                    }).ToList()
                }).ToListAsync();
        }

        public async Task<ProductDTOGet> GetById(int id)
        {
            var product = await _context.Products.
                Where(x => x.Id == id).
                Include(x => x.Users).ThenInclude(x => x.PaymentCard).
                Include(x => x.Category).
                Select(x => new ProductDTOGet
                {
                    Name = x.Name,
                    Price = x.Price,
                    Category = x.Category.Name,
                    Users = x.Users.Select(x => new UserDTOGet
                    {
                        Username = x.Username,
                        Email = x.Email,
                        PaymentCardDTOGet = new PaymentCardDTOGet
                        {
                            CardNumber = x.PaymentCard.CardNumber,
                            HolderName = x.PaymentCard.HolderName,
                            ExpireYear = x.PaymentCard.ExpireYear,
                        }

                    }).ToList()
                }).FirstOrDefaultAsync();
            if (product == null)
                return null;
            return product;
        }

        public async Task<bool> Update(ProductDTOPost dto, int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null) return false;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Description = dto.Description;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            if (dto.Category != null && dto.Category != "")
            {
                var validCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == dto.Category);
                if (validCategory != null)
                {
                    product.Category = validCategory;
                    await _context.SaveChangesAsync();
                }
            }

            if (dto.Users != null && dto.Users.Any()) 
            {
                var validUsers = await _context.Users.Where(x => dto.Users.Contains(x.Username)).ToListAsync();
                if(validUsers.Any() && validUsers != null)
                {
                    product.Users = validUsers;
                    await _context.SaveChangesAsync();
                }
            }
            return true;
        }
    }
}
