namespace WebApplication1.Models.Entities
{
    public class Service
    {
        public int Id { get; set; } 
        public string Title { get; set; } = null!;
        public string Price { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        // Foreign key to Order
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
