using WebApplication1.Models;

namespace WebApplication1.Services.Interfaces
{
    public interface IJSONWebTokenService
    {
        string GenerateJSONWebToken(User user);
    }
}
