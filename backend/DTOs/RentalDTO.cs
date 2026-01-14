using backend.Models;

namespace backend.DTOs
{
    public class RentalDTO
    {
        public DateTime RentalDate { get; set; }
        public string PersonName { get; set; }
        public string PersonPhoneNumber { get; set; }
        public string PersonJMBG { get; set; }
        public string GameId { get; set; }
    }
}
