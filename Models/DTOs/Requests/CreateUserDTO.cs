using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs.Requests
{
    public class CreateUserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
