using WebApplication1.Models.DTOs.Requests;
using WebApplication1.Models.DTOs.Responds;
using WebApplication1.Models.Entities;
using WebApplication1.Repositories.Implementations;
using WebApplication1.Repositories.Interfaces;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IServiceRepository _serviceRepository;

        public OrderService(IOrderRepository repository, IServiceRepository serviceRepository)
        {
            _repository = repository;
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<GetOrderDTO>> GetAllAsync()
        {
            var orders = await _repository.GetAllAsync();

            return orders.Select(o => new GetOrderDTO
            {
                Id = o.Id,
                UserName = o.UserName,
                PhoneNumber = o.PhoneNumber,
                AnimalType = o.AnimalType,
                Breed = o.Breed,
                PetAge = o.PetAge,
                PetWeight = o.PetWeight,
                PreferredDate = o.PreferredDate,
                Message = o.Message,
                IsActive = o.IsActive,
                ServiceTitle = o.Service.Title
            });
        }
        public async Task<GetOrderDTO?> GetByIdAsync(int id)
        {

            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                throw new KeyNotFoundException("Order not found");

            return new GetOrderDTO
            {
                Id = order.Id,
                UserName = order.UserName,
                PhoneNumber = order.PhoneNumber,
                AnimalType = order.AnimalType,
                Breed = order.Breed,
                PetAge = order.PetAge,
                PetWeight = order.PetWeight,
                PreferredDate = order.PreferredDate,
                Message = order.Message,
                IsActive = order.IsActive,
                ServiceTitle = order.Service.Title

            };
        }
        public async Task<GetOrderDTO> AddAsync(CreateOrderDTO dto, string title)
        {
            var service = await _serviceRepository.GetByTitleAsync(title);

            if (service == null)
                throw new KeyNotFoundException("Service not found");

            var order = new Order
            {
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber,
                AnimalType = dto.AnimalType,
                Breed = dto.Breed,
                PetAge = dto.PetAge,
                PetWeight = dto.PetWeight,
                PreferredDate = dto.PreferredDate,
                Message = dto.Message,
                ServiceId = dto.ServiceId
            };

            await _repository.AddAsync(order);

            return new GetOrderDTO
            {
                Id = order.Id,
                UserName = order.UserName,
                PhoneNumber = order.PhoneNumber,
                AnimalType = order.AnimalType,
                Breed = order.Breed,
                PetAge = order.PetAge,
                PetWeight = order.PetWeight,
                PreferredDate = order.PreferredDate,
                Message = order.Message,
                IsActive = order.IsActive,
                ServiceTitle = service.Title
            };
        }
        public async Task DeleteAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                throw new KeyNotFoundException("Order not found");

            await _repository.DeleteAsync(order);
        }
    }
}
