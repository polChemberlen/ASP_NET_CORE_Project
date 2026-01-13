using WebApplication1.Models;

namespace WebApplication1.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(string name); 
    }
}
