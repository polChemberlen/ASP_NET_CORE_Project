using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
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

        [HttpPost]
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
                Password = dto.Password,
                RoleId = dto.RoleId
            };

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

        [HttpPut("{id}")]
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
            user.Password = dto.Password;
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
