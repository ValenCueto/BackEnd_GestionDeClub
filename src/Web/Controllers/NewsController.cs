using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,CM")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost("Create")]
        public IActionResult CreateNews([FromBody] NewsCreateRequest request)
        {
            try
            {
                _newsService.Create(request);
                return Ok("Noticia creada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateNews(int id, [FromBody] NewsCreateRequest request)
        {
            try
            {
                _newsService.Update(id, request);
                return Ok("Noticia actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteNews(int id)
        {
            try
            {
                _newsService.Delete(id);
                return Ok("Noticia eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public ActionResult<List<NewsDtoResponse>> GetAllNews()
        {
            try
            {
                var result = _newsService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<NewsDtoResponse> GetNewsById(int id)
        {
            try
            {
                var result = _newsService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetByDate")]
        public ActionResult<List<NewsDtoResponse>> GetNewsByDate([FromQuery] DateTime date)
        {
            try
            {
                var result = _newsService.GetByDate(date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
