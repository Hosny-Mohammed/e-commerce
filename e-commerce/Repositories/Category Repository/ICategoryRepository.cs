using e_commerce.DTOs;

namespace e_commerce.Repositories.Category_Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDTOGet>> GetAll();
        Task<CategoryDTOGet> GetById(int id);
        Task Add(CategoryDTOPost dto);
        Task<bool> Update(CategoryDTOPost dto, int id);
        Task<bool> Delete(int id);
    }
}
