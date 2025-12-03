using backend.DTOs;
using backend.Models;
using backend.Repos;
namespace backend.Services
{
    public class MechanicService
    {
        public readonly IMechanicRepo _mechanicRepo;

        public MechanicService(IMechanicRepo mechanicRepo)
        {
            _mechanicRepo = mechanicRepo;
        }

        public async Task<IEnumerable<Mechanic>> GetAllMechanics()
        {
            return await _mechanicRepo.GetAllMechanics();
        }

        public async Task<Mechanic> GetMechanicById(string mechanicId)
        {
            return await _mechanicRepo.GetMechanicById(mechanicId);
        }

        public async Task<Mechanic> GetMechanicByName(string mechanicName)
        {
            return await _mechanicRepo.GetMechanicByName(mechanicName);
        }

        public async Task<IEnumerable<Mechanic>>GetMechanicsByGameId(string gameId)
        {
            return await _mechanicRepo.GetMechanicsByGameId(gameId);
        }

        public async Task<Mechanic> CreateMechanic(MechanicDTO mechanic)
        {
            return await _mechanicRepo.CreateMechanic(new Mechanic
            {
                Name= mechanic.Name
            });
        }

        public async Task<Mechanic> UpdateMechanic(string id, MechanicDTO mechanic)
        {
            return await _mechanicRepo.UpdateMechanic(new Mechanic
            {
                Id= id,
                Name= mechanic.Name
            });
        }

        public async Task<Mechanic> DeleteMechanic(string mechanicId)
        {
            return await _mechanicRepo.DeleteMechanic(mechanicId);
        }


    }
}
