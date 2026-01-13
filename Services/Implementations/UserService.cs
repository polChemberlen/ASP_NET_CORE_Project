using Microsoft.AspNetCore.Identity;
using WebApplication1.DTOs.Requests;
using WebApplication1.DTOs.Responds;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;
using WebApplication1.Repositories.Implementations;
using WebApplication1.Repositories.Interfaces;

namespace WebApplication1.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJSONWebTokenService _jsonWebTokenService;
        private readonly IRoleRepository _roleRepository;

        public UserService(
            IUserRepository repository,
            IPasswordHasher<User> passwordHasher, 
            IJSONWebTokenService jsonWebTokenService,
            IRoleRepository roleRepository)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
            _jsonWebTokenService = jsonWebTokenService;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<GetUserDTO>> GetAllUsersAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(u => new GetUserDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                RoleName = u.Role.Name
            });
        }

        public async Task<GetUserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");


            return new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = user.Role.Name
            };
        }

        public async Task<GetUserDTO> RegisterAsync(CreateUserDTO dto)
        {
            var userRole = await _roleRepository.GetByNameAsync("User");

            if (userRole == null)
                throw new KeyNotFoundException("Default role not found");

            var UserExist = await _repository.ExistsByEmailAsync(dto.Email);

            if (UserExist)
                throw new Exception("User with this email already exist");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = userRole.Id
            };

            user.Password = _passwordHasher.HashPassword(user, dto.Password);

            await _repository.AddAsync(user);

            return new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName= userRole.Name
            };
        }

        public async Task<string> LoginAsync(LoginUserDTO dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            var hashCheck = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (hashCheck == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid email or password");

            return _jsonWebTokenService.GenerateJSONWebToken(user);
        }

        public async Task<GetUserDTO> UpdateUserAsync(int id, UpdateUserDTO dto)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.Name = dto.Name;
            user.Email = dto.Email;

            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.Password = _passwordHasher.HashPassword(user, dto.Password);

            await _repository.UpdateAsync(user);

            return new GetUserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
            };
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            await _repository.DeleteAsync(user);
        }
    }
}
