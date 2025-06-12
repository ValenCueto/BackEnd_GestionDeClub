using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("[Action]")]
        public IActionResult Create([FromBody] AvailabilityInitRequest request)
        {
            _availabilityService.InitDefaultAvailability(request);
            return Ok();
        }

        [HttpGet("[Action]")]
        public IActionResult GetAll()
        {
            return Ok(_availabilityService.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[Action]/{day}")]
        public IActionResult UpdateAvailability([FromBody] AvailabilityInitRequest dto, DayOfWeek day)
        {
            try
            {
                _availabilityService.UpdateAvailability(dto, day);
                return Ok("Se ha actualizado la disponibilidad correctamente");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
