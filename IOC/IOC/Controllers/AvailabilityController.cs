using IOC.Models;
using IOC.RequestModels;
using IOC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOC.Controllers
{
    [Route("api")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet("availabilities")]
        public async Task<List<Availability>> GetAllAvailabilities()
        {
            return  await _availabilityService.GetAllAvailabilities();
        }

        [HttpGet]
        [Route("availabilities/{id}")]
        public async Task<ActionResult<Availability>> GetAvailability(int id)
        {
            Availability availibility = await _availabilityService.GetAvailability(id);
            return availibility is not null ? availibility : NotFound(); 
        }

        [HttpPost("availability")]
        public async Task<IActionResult> AddAvailability(CreateAvailabilityRequest createAvailability)
        {
            return new ObjectResult(await _availabilityService.AddAvailability(createAvailability)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete("availability/{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            bool result = await _availabilityService.DeleteAvailability(id);
            if (!result)
                return NotFound("Availability not found");
            return Ok();
        }
    }
}
