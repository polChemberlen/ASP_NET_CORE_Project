using WebApplication1.Models.Entities;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<Service?> GetByTitleAsync(string title);

        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Service service);

        Task<bool> ExistByTitleAsync(string title);
        Task<bool> ExistByIdAsync(int id);
        Task<bool> IsStatusAcive(Service service);
    }
}
