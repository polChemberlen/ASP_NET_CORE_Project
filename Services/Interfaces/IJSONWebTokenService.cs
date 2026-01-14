using WebApplication1.Models.Entities;

namespace WebApplication1.Services.Interfaces
{
    public interface IJSONWebTokenService
    {
        string GenerateJSONWebToken(User user);
    }
}
