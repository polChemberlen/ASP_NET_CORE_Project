namespace WebApplication1.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        //Foreign key to User
        public ICollection<User> Users { get; } = new List<User>();
    }
}
