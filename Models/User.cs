using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        public string Password {  get; set; } = null!;

        // Foreign key to Role
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

    }
}
