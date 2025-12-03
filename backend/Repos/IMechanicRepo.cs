using backend.Models;
namespace backend.Repos
{
    public interface IMechanicRepo
    {
        Task<IEnumerable<Mechanic>> GetAllMechanics();
        Task<Mechanic> GetMechanicById(string id);
        Task<Mechanic> GetMechanicByName(string name);
        Task<Mechanic> CreateMechanic(Mechanic mechanic);
        Task<Mechanic> UpdateMechanic(Mechanic mechanic);
        Task<Mechanic> DeleteMechanic(string id);
        Task<IEnumerable<Mechanic>> GetMechanicsByGameId(string gameId);

    }
}
