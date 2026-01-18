using WebApplication1.Models.Entities;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task DeleteAsync(Order order);
        Task<bool> IsStatusAcive(Order order);
    }
}
