namespace Patrick_T_Assignment_2.Models.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public DateTime PostingDate { get; set; }
        public Guid UserId { get; set; }  // Foreign key for User
        public User User { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
