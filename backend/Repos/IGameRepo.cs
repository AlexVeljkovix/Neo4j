using backend.Models;
namespace backend.Repos
{
    public interface IGameRepo
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameById(string id);
        Task<IEnumerable<Game>> GetGameByTitle(string title);
        Task<Game> CreateGame(Game game, string authorId, string publisherId);
        Task<Game> UpdateGame(Game game);
        Task<Game> DeleteGame(string id);

        Task<IEnumerable<Game>> GetGamesByAuthorId(string authorId);
        Task<IEnumerable<Game>> GetGamesByPublisherId(string publisherId);
        Task<IEnumerable<Game>> GetGamesByMechanicsId(string mechanicsId);

        Task<Game> AddMechanicToGame(string gameId, string mechanicId);
    }
}
