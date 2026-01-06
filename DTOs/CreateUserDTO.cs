using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int RoleId { get; set; }
    }
}
