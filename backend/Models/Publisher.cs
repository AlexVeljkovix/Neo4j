namespace backend.Models
{
    public class Publisher
    {
        public string Id { get; set; } =Guid.NewGuid().ToString();
        public string Name { get; set; }
        public List<Game> Games { get; set; } = new();
    }
}
