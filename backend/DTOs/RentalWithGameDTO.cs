namespace backend.DTOs
{
    public class RentalWithGameDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public bool Active { get; set; } = true;
        public DateTime RentalDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; } = null;
        public string PersonName { get; set; }
        public string PersonPhoneNumber { get; set; }
        public string PersonJMBG { get; set; }
        public string GameId { get; set; }
        public string GameName { get; set; }
    }
}
