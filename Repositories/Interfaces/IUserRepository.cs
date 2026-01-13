using WebApplication1.Models;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);

        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByIdAsync(int id);

    }
}
