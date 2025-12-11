using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAll()
        {
            var games = await _gameService.GetAllGames();
            return Ok(games);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllAvailable()
        {
            var games=await _gameService.GetAllAvailableGames();
            return Ok(games);
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<Game>> GetById(string id)
        {
            var game = await _gameService.GetGameById(id);

            if (game == null)
                return NotFound($"Game with ID {id} not found.");

            return Ok(game);
        }

        [HttpGet("by-authorId/{authorId}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetByAuthorId(string authorId)
        {
            var game = await _gameService.GetGamesByAuthorId(authorId);

            if (game == null)
                return NotFound($"Game with author ID {authorId} not found.");

            return Ok(game);
        }


        [HttpGet("by-publisherId/{publisherId}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetByPublisherId(string publisherId)
        {
            var game = await _gameService.GetGamesByPublisherId(publisherId);

            if (game == null)
                return NotFound($"Game with publisher ID {publisherId} not found.");

            return Ok(game);
        }

        [HttpGet("by-mechanicId/{mechanicsId}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetByMechanicsId(string mechanicsId)
        {
            var game = await _gameService.GetGamesByMechanicsId(mechanicsId);

            if (game == null)
                return NotFound($"Game with mechanics ID {mechanicsId} not found.");

            return Ok(game);
        }

        [HttpGet("by-name")]
        public async Task<ActionResult<IEnumerable<Game>>> GetByName(string gameTitle)
        {
            var games = await _gameService.GetGameByTitle(gameTitle);

            if (!games.Any())
                return NotFound("No games found with that title.");

            return Ok(games);
        }


        [HttpPost]
        public async Task<ActionResult<Game>> Create(GameDTO game)
        {
            if (game == null)
                return BadRequest("Invalid game data.");

            var created = await _gameService.CreateGame(game);

            return Ok(created);
        }

        [HttpPost("{gameId}/mechanics/{mechanicId}")]
        public async Task<IActionResult> AddMechanicToGame(string gameId, string mechanicId)
        {
            var updatedGame = await _gameService.AddMechanicToGame(gameId, mechanicId);

            if (updatedGame == null)
                return NotFound("Game or mechanic not found.");

            return Ok(updatedGame);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> Update(string id, GameDTO game)
        {
           
            var existing = await _gameService.GetGameById(id);
            if (existing == null)
                return NotFound($"Game with ID {id} not found.");

            var updated = await _gameService.UpdateGame(id, game);
            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _gameService.GetGameById(id);
            if (existing == null)
                return NotFound($"Game with ID {id} not found.");

            var deleted = await _gameService.DeleteGame(id);
            return Ok(deleted);
        }



    }
}
