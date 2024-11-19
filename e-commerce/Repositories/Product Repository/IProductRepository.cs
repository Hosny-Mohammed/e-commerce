using e_commerce.DTOs;

namespace e_commerce.Repositories.Product_Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTOGet>> GetAll();
        Task<ProductDTOGet> GetById(int id);
        Task Add(ProductDTOPost dto);
        Task<bool> Update(ProductDTOPost dto, int id);
        Task<bool> Delete(int id);
    }
}
