using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs.Requests;
using WebApplication1.Models.DTOs.Responds;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _services;

        public ServiceController(IServiceService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetServiceDTO>>> GetServices()
        {
            try
            {
                var services = await _services.GetAllAsync();

                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetServiceDTO>> GetService(int id)
        {
            try
            {
                var service = await _services.GetByIdAsync(id);

                return Ok(service);
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetServiceDTO>> CreateService(CreateServiceDTO dto)
        {
            try
            {
                var service = await _services.AddAsync(dto);

                return Ok(service);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GetServiceDTO>> UpdateService(int id, UpdateServiceDTO dto)
        {
            try
            {
                await _services.UpdateAsync(id, dto);

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
        public async Task<ActionResult> DeleteService(int id)
        {
            try
            {
                await _services.DeleteAsync(id);

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