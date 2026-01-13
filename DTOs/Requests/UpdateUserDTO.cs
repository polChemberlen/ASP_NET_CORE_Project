namespace WebApplication1.DTOs.Requests
{
    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
    }
}
