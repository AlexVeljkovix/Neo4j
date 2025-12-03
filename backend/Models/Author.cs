namespace backend.Models
{
    public class Author
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public List<Game> Games { get; set; } = new();
    }
}
