using backend.DTOs;
using backend.Models;

namespace backend.Repos
{
    public interface IRentalRepo
    {
        Task<IEnumerable<RentalWithGameDTO>> GetAllRentals();
        Task<IEnumerable<RentalWithGameDTO>> GetAllActiveRentals();
        Task<RentalWithGameDTO> GetRentalById(string id);
        Task<IEnumerable<RentalWithGameDTO>> GetActiveRentals(string JMBG);
        Task<IEnumerable<RentalWithGameDTO>> GetRentalsByGameId(string gameId);
        Task<Rental> CreateRentalRecord(Rental rental, string gameId);
        Task<RentalWithGameDTO> FinishRentalRecord(string id);
        Task<RentalWithGameDTO> DeleteRentalRecord(string id);

    }
}
