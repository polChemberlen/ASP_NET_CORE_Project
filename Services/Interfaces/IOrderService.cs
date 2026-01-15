using WebApplication1.Models.DTOs.Requests;
using WebApplication1.Models.DTOs.Responds;
using WebApplication1.Models.Entities;

namespace WebApplication1.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrderDTO>> GetAllAsync();
        Task<GetOrderDTO?> GetByIdAsync(int id);
        Task<GetOrderDTO> AddAsync(CreateOrderDTO dto, string title);
        Task DeleteAsync(int id);
    }
}
