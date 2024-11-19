using e_commerce.DTOs;

namespace e_commerce.Repositories.User_Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDTOGet>> GetAll();
        Task<UserDTOGet> GetById(int id);
        Task<bool> Add(UserDTOPost dto);
        Task<bool> Update(UserDTOPost dto, int id);
        Task<bool> Delete(int id);
    }
}
