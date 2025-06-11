using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpPost("Create")]
        [Authorize]
        public IActionResult CreateNews([FromBody] NewsCreateRequest request)
        {
            _newsService.Create(request);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin,CM")]
        public IActionResult UpdateNews(int id, [FromBody] NewsCreateRequest request)
        {
            _newsService.Update(id, request);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = "Admin,CM")]
        public IActionResult DeleteNews(int id)
        {
            _newsService.Delete(id);
            return Ok();
        }

        [HttpGet("GetAll")]
       
        public ActionResult<List<NewsDtoResponse>> GetAllNews()
        {
            var result = _newsService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        [Authorize]
        public ActionResult<NewsDtoResponse> GetNewsById(int id)
        {
            var result = _newsService.GetById(id);
            return Ok(result);
        }

        [HttpGet("GetByDate")]
        [Authorize]
        public ActionResult<List<NewsDtoResponse>> GetNewsByDate([FromQuery] DateTime date)
        {
            var result = _newsService.GetByDate(date);
            return Ok(result);
        }

        [HttpPost("UploadImage")]
        [Authorize(Roles = "Admin,CM")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Archivo no válido.");

            // Asegurarse de que el directorio exista
            var imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            // Evitar conflictos de nombre con un GUID
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(imagesFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imageUrl = $"{Request.Scheme}://{Request.Host}/images/{fileName}";
            return Ok(imageUrl);
        }

    }
}
