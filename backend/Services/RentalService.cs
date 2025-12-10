using backend.DTOs;
using backend.Models;
using backend.Repos;

namespace backend.Services
{
    public class RentalService
    {
        private readonly IRentalRepo _rentalRepo;

        public RentalService(IRentalRepo rentalRepo)
        {
            _rentalRepo = rentalRepo;
        }

        public async Task<IEnumerable<RentalWithGameDTO>> GetAllRentals()
        {
            return await _rentalRepo.GetAllRentals();
        }
        public async Task<IEnumerable<RentalWithGameDTO>> GetAllActiveRentals()
        {
            return await _rentalRepo.GetAllActiveRentals();
        }
        public async Task<RentalWithGameDTO> GetRentalById(string id)
        {
            return await _rentalRepo.GetRentalById(id);
        }
        public async Task<IEnumerable<RentalWithGameDTO>> GetActiveRentals(string JMBG)
        {
            return await _rentalRepo.GetActiveRentals(JMBG);
        }
        public async Task<IEnumerable<RentalWithGameDTO>> GetRentalsByGameId(string gameId)
        {
            return await _rentalRepo.GetRentalsByGameId(gameId);
        }
        public async Task<Rental> CreateRentalRecord(RentalDTO rental)
        {
            
                return await _rentalRepo.CreateRentalRecord(new Rental
                {
                    RentalDate = rental.RentalDate,
                    PersonName = rental.PersonName,
                    PersonPhoneNumber = rental.PersonPhoneNumber,
                    PersonJMBG = rental.PersonJMBG,
                }, rental.GameId);
            
            throw new Exception("No available units for that game");
            
        }


        public async Task<RentalWithGameDTO> FinishRentalRecord(string id)
        {
            return await _rentalRepo.FinishRentalRecord(id);
        }
        public async Task<RentalWithGameDTO> DeleteRentalRecord(string id)
        {
            return await _rentalRepo.DeleteRentalRecord(id);
        }
    }
}
