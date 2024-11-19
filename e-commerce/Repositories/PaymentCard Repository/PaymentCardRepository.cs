using e_commerce.Data;
using e_commerce.DTOs;
using e_commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce.Repositories.PaymentCard_Repository
{
    public class PaymentCardRepository : IPaymentCardRepository
    {
        private readonly AppDbContext _context;
        public PaymentCardRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task Add(PaymentCardDTOPost dto)
        {
            PaymentCard paymentCard = new PaymentCard() 
            {
                HolderName = dto.HolderName,
                CardNumber = dto.CardNumber,
                ExpireYear = dto.ExpireYear,
            };
            await _context.AddAsync(paymentCard);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
           var paymentCard = await _context.PaymentCards.FindAsync(id);
            if (paymentCard == null) 
                return false;
            _context.PaymentCards.Remove(paymentCard);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PaymentCardDTOGet>> GetAll()
        {
            return await _context.PaymentCards.Select(x => new PaymentCardDTOGet() 
            { 
                CardNumber = x.CardNumber,
                HolderName=x.HolderName,
                ExpireYear=x.ExpireYear,
            }).ToListAsync();
        }

        public async Task<PaymentCardDTOGet> GetById(int id)
        {
            var paymentCard = await _context.PaymentCards.Where(x => x.Id == id).Select(x => new PaymentCardDTOGet()
            {
                CardNumber = x.CardNumber,
                HolderName = x.HolderName,
                ExpireYear = x.ExpireYear,
            }).FirstOrDefaultAsync();
            if (paymentCard == null)
                return null;
            return paymentCard;
        }

        public async Task<bool> Update(PaymentCardDTOPost dto, int id)
        {
            var paymentCard = await _context.PaymentCards.FindAsync(id);
            if (paymentCard == null) return false;
            paymentCard.CardNumber = dto.CardNumber;
            paymentCard.HolderName = dto.HolderName;
            paymentCard.ExpireYear = dto.ExpireYear;
            
            _context.PaymentCards.Update(paymentCard);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
