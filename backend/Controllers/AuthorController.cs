using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;


namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAll()
        {
            var authors = await _authorService.GetAllAuthors();
            return Ok(authors);
        }

        
        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<Author>> GetById(string id)
        {
            var author = await _authorService.GetAuthorById(id);

            if (author == null)
                return NotFound($"Author with ID {id} not found.");

            return Ok(author);
        }

        [HttpGet("by-name")]
        public async Task<ActionResult<IEnumerable<Author>>> GetByName(string firstName, string lastName)
        {
            var authors = await _authorService.GetAuthorByName(firstName, lastName);

            if (!authors.Any())
                return NotFound("No authors found with that name.");

            return Ok(authors);
        }

        [HttpGet("by-gameId/{gameId}")]
        public async Task<ActionResult<Author>>GetByGameId(string gameId)
        {
            var author=await _authorService.GetAuthorByGameId(gameId);
            if (author == null)
                return NotFound($"Author with game ID {gameId} not found");
            return Ok(author); 
        }

       
        [HttpPost]
        public async Task<ActionResult<Author>> Create(AuthorDTO author)
        {
            if (author == null)
                return BadRequest("Invalid author data.");

            var created = await _authorService.CreateAuthor(author);
            return Ok(created);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<Author>> Update(string id, AuthorDTO author)
        {
            
            var existing = await _authorService.GetAuthorById(id);
            if (existing == null)
                return NotFound($"Author with ID {id} not found.");

            var updated = await _authorService.UpdateAuthor(id, author);
            return Ok(updated);
        }

        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _authorService.GetAuthorById(id);
            if (existing == null)
                return NotFound($"Author with ID {id} not found.");

            var deleted = await _authorService.DeleteAuthor(id);
            return Ok(deleted);
        }
    }
}
