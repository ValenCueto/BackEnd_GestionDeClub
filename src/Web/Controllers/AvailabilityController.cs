using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
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

        [HttpPost("[Action]")]
        public IActionResult Create([FromBody] AvailabilityInitRequest request)
        {
            try
            {
                _availabilityService.InitDefaultAvailability(request);
                return Ok("Se han creado las disponibilidades correctamente");
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[Action]")]
        public IActionResult GetAll()
        {
            return Ok(_availabilityService.GetAll());
        }

        [HttpPut("[Action]/{day}")]
        public IActionResult UpdateAvailability([FromBody] AvailabilityInitRequest dto, string day)
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
