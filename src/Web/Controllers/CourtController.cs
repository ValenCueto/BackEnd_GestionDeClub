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
            return Ok(court);
        }


        [HttpPost]
        public IActionResult CreateCourt()
        {
            _courtService.CreateCourt();
            return Ok("La cancha fue creada correctamente");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCourt(int id)
        {
            _courtService.DeleteCourt(id);
            return Ok();
        }
    }
}