namespace WebApplication1.Models.DTOs.Requests
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
    }
}
