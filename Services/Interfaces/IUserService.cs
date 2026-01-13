
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responds;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<GetUserDTO>> GetAllUsersAsync();
        Task<GetUserDTO?> GetUserByIdAsync(int id);

        Task<GetUserDTO> RegisterAsync (CreateUserDTO dto);

        Task<string> LoginAsync(LoginUserDTO dto);

        Task<GetUserDTO> UpdateUserAsync(int id, UpdateUserDTO dto);
        Task DeleteUserAsync(int id);
    }
}