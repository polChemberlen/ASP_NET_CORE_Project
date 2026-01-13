using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Models;
using WebApplication1.Options;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Implementations
{
    public class JSONWebTokenService : IJSONWebTokenService
    {

        private readonly JwtOptions _JwtOptions;

        public JSONWebTokenService(IOptions<JwtOptions> options)
        {
            _JwtOptions = options.Value;
        }

        public string GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtOptions.Key));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim (JwtRegisteredClaimNames.Email,  user.Email),
        new Claim (ClaimTypes.Role, user.Role.Name)
    };

            var token = new JwtSecurityToken(
                issuer: _JwtOptions.Issuer,
                audience: _JwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_JwtOptions.ExpiresMinutes),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
