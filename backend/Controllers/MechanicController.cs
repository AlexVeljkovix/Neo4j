using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicController : ControllerBase
    {
        public readonly MechanicService _mechanicService;

        public MechanicController(MechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mechanic>>> GetAll()
        {
            var mechanics=await _mechanicService.GetAllMechanics();
            return Ok(mechanics);
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<Mechanic>> GetById(string id)
        {
            var mechanic = await _mechanicService.GetMechanicById(id);

            if (mechanic == null)
                return NotFound($"Mechanic with ID {id} not found.");

            return Ok(mechanic);
        }

        [HttpGet("by-name")]
        public async Task<ActionResult<IEnumerable<Mechanic>>> GetByName(string name)
        {
            var mechanic = await _mechanicService.GetMechanicByName(name);

            if (mechanic==null)
                return NotFound("No mechanic found with that name.");

            return Ok(mechanic);
        }

        [HttpGet("by-gameId/{gameId}")]
        public async Task<ActionResult<IEnumerable<Mechanic>>> GetByGameId(string gameId)
        {
            var mechanics = await _mechanicService.GetMechanicsByGameId(gameId);
            if (!mechanics.Any())
                return NotFound($"Mechanic with game ID {gameId} not found");
            return Ok(mechanics);
        }


        [HttpPost]
        public async Task<ActionResult<Mechanic>> Create(MechanicDTO mechanic)
        {
            if (mechanic == null)
                return BadRequest("Invalid mechanic data.");

            var created = await _mechanicService.CreateMechanic(mechanic);

            return Ok(created);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Mechanic>> Update(string id, MechanicDTO mechanic)
        {
            
            var existing = await _mechanicService.GetMechanicById(id);
            if (existing == null)
                return NotFound($"Author with ID {id} not found.");

            var updated = await _mechanicService.UpdateMechanic(id, mechanic);
            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var existing = await _mechanicService.GetMechanicById(id);
            if (existing == null)
                return NotFound($"Mechanic with ID {id} not found.");

            var deleted = await _mechanicService.DeleteMechanic(id);
            return Ok(deleted);
        }

    }
}
