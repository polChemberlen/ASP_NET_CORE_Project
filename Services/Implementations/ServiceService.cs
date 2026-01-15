using WebApplication1.Models.DTOs.Requests;
using WebApplication1.Models.DTOs.Responds;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetServiceDTO>> GetAllAsync()
        {
            var services = await _repository.GetAllAsync();

            return services.Select(s => new GetServiceDTO
            {
                Id = s.Id,
                Title = s.Title,
                Price = s.Price,
                Description = s.Description,
                IsActive = s.IsActive
            });
        }

        public async Task<GetServiceDTO?> GetByIdAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service == null)
                throw new KeyNotFoundException("Service not found");

            return new GetServiceDTO
            {
                Id = service.Id,
                Title = service.Title,
                Price = service.Price,
                Description = service.Description,
                IsActive = service.IsActive
            };
        }

        public async Task<GetServiceDTO?> GetByTitleAsync(string title)
        {
            var service = await _repository.GetByTitleAsync(title);

            return new GetServiceDTO
            {
                Id = service.Id,
                Title = service.Title,
                Price = service.Price,
                Description = service.Description,
                IsActive = service.IsActive
            };
        }

        public async Task<GetServiceDTO> AddAsync(CreateServiceDTO dto)
        {
            var ServiceExist = await _repository.ExistByTitleAsync(dto.Title);

            if (ServiceExist)
                throw new Exception("Service with this title already exist");

            var service = new Service
            {
                Title = dto.Title,
                Price = dto.Price,
                Description = dto.Description
            };

            await _repository.AddAsync(service);

            return new GetServiceDTO
            {
                Id = service.Id,
                Title = service.Title,
                Price = service.Price,
                Description = service.Description
            };
        }

        public async Task<GetServiceDTO> UpdateAsync(int id, UpdateServiceDTO dto)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service == null)
                throw new KeyNotFoundException("Service not found");

            service.Title = dto.Title;
            service.Price = dto.Price;
            service.Description = dto.Description;
            service.IsActive = dto.IsActive;

            await _repository.UpdateAsync(service);

            return new GetServiceDTO
            {
                Id = service.Id,
                Title = service.Title,
                Price = service.Price,
                Description = service.Description
            };
        }

        public async Task DeleteAsync(int id)
        {
            var service = await _repository.GetByIdAsync(id);

            if (service == null)
                throw new KeyNotFoundException("Service not found");

            await _repository.DeleteAsync(service);
        }
    }
}