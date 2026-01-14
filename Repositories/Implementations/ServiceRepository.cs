using WebApplication1.Models.Entities;
using WebApplication1.Data;
using WebApplication1.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Service?> GetByTitleAsync(string title)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Title == title);
        }

        public async Task AddAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistByTitleAsync(string title)
        {
            return await _context.Services.AnyAsync(s => s.Title == title);
        }
        public async Task<bool> ExistByIdAsync(int id)
        {
            return await _context.Services.AnyAsync(s => s.Id == id);
        }
    }
}
