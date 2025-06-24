// Controller actualizado para usar el servicio de imágenes
using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Infrastructure.Services; // Agregar este using
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;
        private readonly IImageService _imageService; // Nuevo servicio

        public NewsController(INewsService newsService, IImageService imageService)
        {
            _newsService = newsService;
            _imageService = imageService;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin,CM")]
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
            try
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Archivo no válido.");

                // Validar tipo de archivo
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };
                if (!allowedTypes.Contains(file.ContentType.ToLower()))
                    return BadRequest("Tipo de archivo no permitido. Solo se permiten imágenes.");

                // Validar tamaño (max 5MB)
                if (file.Length > 5 * 1024 * 1024)
                    return BadRequest("El archivo es demasiado grande. Máximo 5MB.");

                var imageUrl = await _imageService.UploadImageAsync(file);
                return Ok(new { url = imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al subir la imagen: {ex.Message}");
            }
        }

        [HttpDelete("DeleteImage")]
        [Authorize(Roles = "Admin,CM")]
        public async Task<IActionResult> DeleteImage([FromQuery] string imageId)
        {
            try
            {
                var result = await _imageService.DeleteImageAsync(imageId);
                return result ? Ok("Imagen eliminada correctamente") : BadRequest("No se pudo eliminar la imagen");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la imagen: {ex.Message}");
            }
        }
    }
}