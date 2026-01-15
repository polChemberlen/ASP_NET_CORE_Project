using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTOs.Requests;
using WebApplication1.Models.DTOs.Responds;
using WebApplication1.Services.Interfaces;
using WebApplication1.Models.Entities;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _services;

        public OrderController(IOrderService services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOrderDTO>>> GetOrders()
        {
            try
            {
                var orders = await _services.GetAllAsync();

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetOrderDTO>> GetOrder(int id)
        {
            try
            {
                var order = await _services.GetByIdAsync(id);

                return Ok(order);
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
        [Authorize]
        public async Task<ActionResult<GetOrderDTO>> CreateOrder(CreateOrderDTO dto, string title)
        {
            try
            {
                var order = await _services.AddAsync(dto, title);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteOrder(int id)
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
