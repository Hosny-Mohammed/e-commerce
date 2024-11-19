using e_commerce.DTOs;

namespace e_commerce.Repositories.PaymentCard_Repository
{
    public interface IPaymentCardRepository
    {
        Task<IEnumerable<PaymentCardDTOGet>> GetAll();
        Task<PaymentCardDTOGet> GetById(int id);
        Task Add(PaymentCardDTOPost dto);
        Task<bool> Update(PaymentCardDTOPost dto, int id);
        Task<bool> Delete(int id);

    }
}
