using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApplication1.Data;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responds;
using WebApplication1.Models;
using WebApplication1.Options;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtOptions _JwtOptions;
        private readonly IUserService _service;

        public UserController(IOptions<JwtOptions> options, IUserService service)
        {
            _JwtOptions = options.Value;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserDTO>>> GetUsers()
        {
            try
            {
                var users = await _service.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserDTO>> GetUser(int id)
        {
            try
            {
                var user = await _service.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("register")]
        public async Task<ActionResult<GetUserDTO>> CreateUser(CreateUserDTO dto)
        {
            try
            {
                var user = await _service.RegisterAsync(dto);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<GetUserDTO>> LoginUser(LoginUserDTO dto)
        {
            try
            {
                var token = await _service.LoginAsync(dto);
                SetJWTCookie(token);
                return Ok();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        private void SetJWTCookie(string token)
        {
            var CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(_JwtOptions.ExpiresMinutes),
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("jwt_token", token, CookieOptions);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateUser(int id, UpdateUserDTO dto)
        {
            try
            {
                await _service.UpdateUserAsync(id, dto);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _service.DeleteUserAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}