using WebApplication1.Models.Entities;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name); 
    }
}
