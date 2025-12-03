namespace backend.Models
{
    public class Game
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; } 

        public string Description { get; set; }
        public List<Mechanic> Mechanics { get; set; } = new();
        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
    }
}
