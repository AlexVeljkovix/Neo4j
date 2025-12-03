
using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        public readonly PublisherService _publisherService;

        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetAll()
        {
            var publishers = await _publisherService.GetAllPublishers();
            return Ok(publishers);
        }


        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<Publisher>> GetById(string id)
        {
            var publisher = await _publisherService.GetPublisherById(id);

            if (publisher == null)
                return NotFound($"Publisher with ID {id} not found.");

            return Ok(publisher);
        }

        [HttpGet("by-name")]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetByName(string name)
        {
            var publishers = await _publisherService.GetPublisherByName(name);

            if (!publishers.Any())
                return NotFound("No publishers found with that name.");

            return Ok(publishers);
        }

        [HttpGet("by-gameId/{gameId}")]
        public async Task<ActionResult<Publisher>> GetByGameId(string gameId)
        {
            var publisher = await _publisherService.GetPublisherByGameId(gameId);
            if (publisher == null)
                return NotFound($"Publisher with game ID {gameId} not found");
            return Ok(publisher);
        }


        [HttpPost]
        public async Task<ActionResult<Publisher>> Create(PublisherDTO publisher)
        {
            if (publisher == null)
                return BadRequest("Invalid publisher data.");

            var created = await _publisherService.CreatePublisher(publisher);

            return Ok(created);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Publisher>> Update(string id, PublisherDTO publisher)
        {
            
            var existing = await _publisherService.GetPublisherById(id);
            if (existing == null)
                return NotFound($"Publisher with ID {id} not found.");

            var updated = await _publisherService.UpdatePublisher(id, publisher);
            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _publisherService.GetPublisherById(id);
            if (existing == null)
                return NotFound($"Publisher with ID {id} not found.");

            var deleted = await _publisherService.DeletePublisher(id);
            return Ok(deleted);
        }
    }
}
