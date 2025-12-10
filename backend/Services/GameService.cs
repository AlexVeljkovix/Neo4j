using backend.DTOs;
using backend.Models;
using backend.Repos;

namespace backend.Services
{
    public class GameService
    {
        public readonly IGameRepo _gameRepo;
        public GameService(IGameRepo gameRepo)
        {
            _gameRepo = gameRepo;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _gameRepo.GetAllGames();
        }

        public async Task<Game> GetGameById(string authorId)
        {
            return await _gameRepo.GetGameById(authorId);
        }

        public async Task<IEnumerable<Game>> GetGameByTitle(string gameTitle)
        {
            return await _gameRepo.GetGameByTitle(gameTitle);
        }


        public async Task<IEnumerable<Game>> GetGameByAuthorId(string authorId)
        {
            return await _gameRepo.GetGamesByAuthorId(authorId);
        }

        public async Task<IEnumerable<Game>> GetGameByPublisherId(string publisherId)
        {
            return await _gameRepo.GetGamesByPublisherId(publisherId);
        }

        public async Task<IEnumerable<Game>> GetGameByMechanicsId(string mechanicsId)
        {
            return await _gameRepo.GetGamesByMechanicsId(mechanicsId);
        }

        public async Task<Game> CreateGame(GameDTO game)
        {
            
            return await _gameRepo.CreateGame(new Game
            {
                Title=game.Title,
                Description=game.Description,
                Difficulty=game.Difficulty,
                AvailableUnits=game.AvailableUnits,
            }, game.MechanicIds, game.AuthorId,game.PublisherId);
        }

        public async Task<Game> UpdateGame(string id, GameDTO game)
        {
            return await _gameRepo.UpdateGame(new Game
            {
                Id=id,
                Title=game.Title,
                Description= game.Description,
                Difficulty = game.Difficulty,
                AvailableUnits = game.AvailableUnits
            });
        }

        public async Task<Game> DeleteGame(string authorId)
        {
            return await _gameRepo.DeleteGame(authorId);
        }

        public async Task<Game> AddMechanicToGame(string gameId, string mechanicsId)
        {
            return await _gameRepo.AddMechanicToGame(gameId, mechanicsId);
        }
    }
}
