using backend.Models;

namespace backend.DTOs
{
    public class RentalDTO
    {
        //public bool Active { get; set; } = true;
        public DateTime RentalDate { get; set; }
        //public DateTime? ReturnDate { get; set; } = null;
        public string PersonName { get; set; }
        public string PersonPhoneNumber { get; set; }
        public string PersonJMBG { get; set; }
        public string GameId { get; set; }
    }
}
