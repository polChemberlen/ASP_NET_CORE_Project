using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs.Requests
{
    public class CreateServiceDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
       
    }
}
