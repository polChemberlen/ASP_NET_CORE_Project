namespace WebApplication1.Models.DTOs.Responds
{
    public class GetOrderDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string AnimalType { get; set; }

        public string? Breed { get; set; }

        public string? PetAge { get; set; }

        public string? PetWeight { get; set; }

        public string PreferredDate { get; set; } 

        public string? Message { get; set; }

        public bool IsActive { get; set; } = true;

        public string ServiceTitle { get; set; }
    }
}
