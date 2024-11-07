namespace Patrick_T_Assignment_2.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
