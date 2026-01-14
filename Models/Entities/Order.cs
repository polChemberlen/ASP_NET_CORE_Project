namespace WebApplication1.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Breed { get; set; } = null!;

        public string? PetAge { get; set; } = null!;

        public string? PetWeight { get; set; } = null!;

        public string PreferredDate { get; set; } = null!;

        public string? Message { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        // Foreign key to Service

        public int ServiceId { get; set; }

        public Service Service { get; set; } = null!;

    }
}
