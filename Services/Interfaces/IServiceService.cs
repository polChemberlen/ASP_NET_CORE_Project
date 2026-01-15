using WebApplication1.Models.DTOs.Requests;
using WebApplication1.Models.DTOs.Responds;
using WebApplication1.Models.Entities;

namespace WebApplication1.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<GetServiceDTO>> GetAllAsync();
        Task<GetServiceDTO?> GetByIdAsync(int id);
        Task<GetServiceDTO?> GetByTitleAsync(string title);

        Task<GetServiceDTO> AddAsync(CreateServiceDTO dto);
        Task<GetServiceDTO> UpdateAsync(int id, UpdateServiceDTO dto);
        Task DeleteAsync(int id);
    }
}
