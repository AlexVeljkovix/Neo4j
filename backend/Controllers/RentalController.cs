using backend.DTOs;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly RentalService _rentalService;

        public RentalController(RentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalWithGameDTO>>> GetAll()
        {
            var rentals = await _rentalService.GetAllRentals();
            return Ok(rentals);
        }
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<RentalWithGameDTO>>> GetAllActive()
        {
            var rentals = await _rentalService.GetAllActiveRentals();
            return Ok(rentals);
        }

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<RentalWithGameDTO>> GetById(string id)
        {
            var rental = await _rentalService.GetRentalById(id);

            if (rental == null)
                return NotFound($"Rental with ID {id} not found.");

            return Ok(rental);
        }

        [HttpGet("active/{JMBG}")]
        public async Task<ActionResult<IEnumerable<RentalWithGameDTO>>> GetActiveRentals(string JMBG)
        {
            var rentals = await _rentalService.GetActiveRentals(JMBG);

            if (!rentals.Any())
                return NotFound("No active rentals found for that JMBG.");

            return Ok(rentals);
        }

        [HttpGet("by-gameId/{gameId}")]
        public async Task<ActionResult<IEnumerable<RentalWithGameDTO>>> GetRentalsByGameId(string gameId)
        {
            var rentals = await _rentalService.GetRentalsByGameId(gameId);

            if (!rentals.Any())
                return NotFound("No rentals found for that game ID.");

            return Ok(rentals);
        }

        [HttpPost]
        public async Task<ActionResult<Rental>> CreateRentalRecord(RentalDTO rental)
        {

            if (rental == null)
                return BadRequest("Invalid rental data.");

            var created = await _rentalService.CreateRentalRecord(rental);
            return Ok(created);
            
        }

        [HttpPut("finish/{id}")]
        public async Task<ActionResult<RentalWithGameDTO>> FinishRentalRecord(string id)
        {
            var finishedRental = await _rentalService.FinishRentalRecord(id);

            if (finishedRental == null)
                return NotFound($"Rental with ID {id} not found.");

            return Ok(finishedRental);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RentalWithGameDTO>> DeleteRentalRecord(string id)
        {
            var deletedRental = await _rentalService.DeleteRentalRecord(id);
            if (deletedRental == null)
                return NotFound($"Rental with ID {id} not found.");
            return Ok(deletedRental);

        }
    }
}
