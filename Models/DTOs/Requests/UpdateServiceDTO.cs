namespace WebApplication1.Models.DTOs.Requests
{
    public class UpdateServiceDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public bool IsActive { get; set; }
        public string ImageUrl { get; set; }
    }
}
