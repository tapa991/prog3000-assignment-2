namespace Patrick_T_Assignment_2.Models.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
    }
}
