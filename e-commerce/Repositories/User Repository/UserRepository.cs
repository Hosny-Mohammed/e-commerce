using e_commerce.Data;
using e_commerce.DTOs;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories.User_Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) { _context = context; }
        public async Task<bool> Add(UserDTOPost dto)
        {
            var check = await _context.Users.FirstOrDefaultAsync(x => x.Username == dto.Username);
            if(check != null)
                return false;
            User user = new User() 
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = dto.Password,
            };
            //await _context.Users.AddAsync(user);
            //await _context.SaveChangesAsync();
            if (dto.Products != null && dto.Products.Any())
            {
                var validProducts = await _context.Products.Where(x => dto.Products.Contains(x.Name)).ToListAsync();
                if (validProducts.Any())
                {
                    user.Products = validProducts;
                   // await _context.SaveChangesAsync();
                }
            }
            if(dto.PaymentCardDTOPost != null)
            {
                var isExist = await _context.PaymentCards.FirstOrDefaultAsync(x => x.CardNumber == dto.PaymentCardDTOPost.CardNumber);
                if (isExist == null)
                {
                    PaymentCard paymentCard = new PaymentCard() 
                    {
                        CardNumber = dto.PaymentCardDTOPost.CardNumber,
                        HolderName = dto.PaymentCardDTOPost.HolderName,
                        ExpireYear = dto.PaymentCardDTOPost.ExpireYear,

                    };
                    //await _context.PaymentCards.AddAsync(paymentCard);
                    user.PaymentCard = paymentCard;
                    //await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("This Credit Is already Used");
                }
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) 
                return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<UserDTOGet>> GetAll()
        {
             return await _context.Users.
                Include(x => x.PaymentCard).
                Include(x => x.Products).
                ThenInclude(x => x.Category).
                Select(x => new UserDTOGet
                {
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                    Products = x.Products.Select(x => new ProductDTOGet
                    {
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description,
                        Category = x.Category.Name,
                    }).ToList(),
                    PaymentCardDTOGet = new PaymentCardDTOGet
                    {
                        CardNumber = x.PaymentCard.CardNumber,
                        HolderName = x.PaymentCard.HolderName,
                        ExpireYear = x.PaymentCard.ExpireYear,
                    }
                }).ToListAsync();
        }

        public Task<UserDTOGet> GetById(int id)
        {
             var user = _context.Users.Where(x => x.Id == id).
                Include(x => x.PaymentCard).
                Include(x => x.Products).
                ThenInclude(x => x.Category).
                Select(x => new UserDTOGet
                {
                    Username = x.Username,
                    Email = x.Email,
                    Password = x.Password,
                    Products = x.Products.Select(x => new ProductDTOGet
                    {
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description,
                        Category = x.Category.Name,
                    }).ToList(),
                    PaymentCardDTOGet = new PaymentCardDTOGet
                    {
                        CardNumber = x.PaymentCard.CardNumber,
                        HolderName = x.PaymentCard.HolderName,
                        ExpireYear = x.PaymentCard.ExpireYear,
                    }
                }).FirstOrDefaultAsync();
            if (user == null)
                return null;
            return user;
        }

        public async Task<bool> Update(UserDTOPost dto, int id)
        {
            var user = await _context.Users.FindAsync(id);  
            if (user == null) return false;
            {
                user.Email = dto.Email;
                user.Password = dto.Password;
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            if (dto.Products != null && dto.Products.Any())
            { 
                var validProducts = await _context.Products.Where(x => dto.Products.Contains(x.Name)).ToListAsync();
                if (validProducts.Any())
                {
                    user.Products = validProducts;
                    await _context.SaveChangesAsync();
                }
            }
            if(dto.PaymentCardDTOPost != null)
            {
                var validPaymentCard = _context.PaymentCards.Where(x => x.CardNumber == dto.PaymentCardDTOPost.CardNumber );
                if (validPaymentCard.Any())
                    throw new Exception("This Credit Is already Used");
                if (!validPaymentCard.Any() && validPaymentCard == null)
                {
                    PaymentCard paymentCard = new PaymentCard() 
                    {
                        HolderName = dto.PaymentCardDTOPost.HolderName,
                        CardNumber = dto.PaymentCardDTOPost.CardNumber,
                        ExpireYear = dto.PaymentCardDTOPost.ExpireYear
                    };
                    user.PaymentCard = paymentCard;
                    await _context.SaveChangesAsync();
                    return true;
                }

                user.PaymentCard.HolderName = dto.PaymentCardDTOPost.HolderName;
                user.PaymentCard.CardNumber = dto.PaymentCardDTOPost.CardNumber;
                user.PaymentCard.ExpireYear = dto.PaymentCardDTOPost.ExpireYear;
                await _context.SaveChangesAsync();
            }
            return true;
        }
    }
}
