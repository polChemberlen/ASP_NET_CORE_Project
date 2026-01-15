using WebApplication1.Models.Entities;
using WebApplication1.Data;
using WebApplication1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(o => o.Service).ToListAsync();
        }
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Service)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> IsStatusAcive(Order order)
        {
            return await _context.Orders
                .AnyAsync(o => o.Id == order.Id && o.IsActive == true);
        }
    }
}
