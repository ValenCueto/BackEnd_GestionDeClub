using Application.Interfaces;
using Application.Models.Request;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourtController : ControllerBase
    {
        private readonly ICourtService _courtService;

        public CourtController(ICourtService courtService)
        {
            _courtService = courtService;
        }

        [HttpGet]
        public IActionResult GetAllCourts()
        {
            return Ok(_courtService.GetAll());
        }


        [HttpGet("{id}")]
        public IActionResult GetCourtById(int id)
        {
            var court = _courtService.GetById(id);

            if (court == null)
            {
                return NotFound($"No se encontró la cancha con ID: {id}");
            }

            return Ok(court);
        }


        [HttpPost]
        public IActionResult CreateCourt()
        {
            try
            {
                _courtService.CreateCourt();
                return Ok("La cancha fue creada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourt(int id)
        {
            try
            {
                _courtService.DeleteCourt(id);
                return Ok("La cancha fue eliminada correctamente");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}