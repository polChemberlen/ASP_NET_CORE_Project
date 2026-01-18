using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.DTOs.Requests
{
    public class CreateOrderDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string AnimalType { get; set; }
        
        public string? Breed { get; set; }

        public string? PetAge { get; set; }

        public string? PetWeight { get; set; }

        [Required]
        public string PreferredDate { get; set; }

        public string? Message { get; set; }

        [Required]
        public int ServiceId { get; set; }
    }
}
