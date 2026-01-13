using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.Requests
{
    public class LoginUserDTO
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
