namespace backend.Models
{
    public class Mechanic
    {
        public string Id { get; set; }=Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}
