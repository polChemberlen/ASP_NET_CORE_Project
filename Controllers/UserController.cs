using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Options;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly JwtOptions _JwtOptions;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserController(ApplicationDbContext context, IOptions<JwtOptions> options, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _JwtOptions = options.Value;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Role)
                .Select(u => new GetUserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    RoleName = u.Role.Name
                }).ToListAsync();

            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Id == id)
                .Select(u => new GetUserDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    RoleName = u.Role.Name
                }).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("User not found");
            }

            return user;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GetUserDTO>> CreateUser(CreateUserDTO dto)
        {
            var RoleExist = await _context.Roles.AnyAsync(r => r.Id == dto.RoleId);

            if (!RoleExist)
            {
                return BadRequest("Role does not exist");
            }

            var UserExist = await _context.Users.AnyAsync(u => u.Email == dto.Email);

            if (UserExist)
            {
                return Conflict("User with this email already Exist");
            }

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = dto.RoleId
            };

            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = null
            });
        }


        [HttpPost("login")]
        public async Task<ActionResult<GetUserDTO>> LoginUser(LoginUserDTO dto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid email or password");
            }

            var hashCheck = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (hashCheck == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid email or password");
            }

            var accessToken = GenerateJSONWebToken(user);

            return Ok(new { Token = accessToken });
        }

        private string GenerateJSONWebToken(User user)
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
                expires: DateTime.Now.AddMinutes(_JwtOptions.ExpiresMinutes),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetJWTCookie(string token)
        {
            var CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(_JwtOptions.ExpiresMinutes)
            };

            Response.Cookies.Append("jwt_token", token, CookieOptions);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(int id, CreateUserDTO dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var RoleExist = await _context.Roles.AnyAsync(r => r.Id == dto.RoleId);

            if (!RoleExist)
            {
                return BadRequest("Role does not exist");
            }

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Password = _passwordHasher.HashPassword(user, dto.Password);
            user.RoleId = dto.RoleId;


            await _context.SaveChangesAsync();

            return Ok(new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = null
            });


        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
