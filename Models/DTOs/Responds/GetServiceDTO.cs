namespace WebApplication1.Models.DTOs.Responds
{
    public class GetServiceDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
