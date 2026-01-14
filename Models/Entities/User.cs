using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;

        // Password should be hashed, not stored in plain text
        public string HashPassword {  get; set; } = null!;

        // Foreign key to Role
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

    }
}
