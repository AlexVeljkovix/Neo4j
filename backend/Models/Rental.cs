namespace backend.Models
{
    public class Rental
    {
        public string Id { get; set; } =Guid.NewGuid().ToString();
        public bool Active { get; set; }= true;
        public DateTime RentalDate { get; set; }=DateTime.Now;
        public DateTime? ReturnDate { get; set; } = null;
        public string PersonName { get; set; }
        public string PersonPhoneNumber { get; set; }
        public string PersonJMBG { get; set; }
    }
}
