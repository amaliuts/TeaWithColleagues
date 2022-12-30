using IOC.Models;
using IOC.RequestModels;
using IOC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet("get-all-availabilities")]
        public async Task<List<Availability>> GetAllAvailabilities()
        {
            return  await _availabilityService.GetAllAvailabilities();
        }

        [HttpGet]
        [Route("get-availability-by-id")]
        public async Task<ActionResult<Availability>> GetAvailability(int id)
        {
            Availability availibility = await _availabilityService.GetAvailability(id);
            return availibility is not null ? availibility : NotFound(); 
        }

        [HttpPost("add-availability")]
        public async Task<IActionResult> AddAvailability(CreateAvailabilityRequest createAvailability)
        {
            return new ObjectResult(await _availabilityService.AddAvailability(createAvailability)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete("delete-availability")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            bool result = await _availabilityService.DeleteAvailability(id);
            if (!result)
                return NotFound("Availability not found");
            return Ok();
        }
    }
}
