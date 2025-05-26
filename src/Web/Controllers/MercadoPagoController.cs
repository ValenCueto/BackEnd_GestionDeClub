using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MercadoPagoController : ControllerBase
    {
        private readonly IMercadoPagoService _mercadoPagoService;

        public MercadoPagoController(IMercadoPagoService mercadoPagoService)
        {
            _mercadoPagoService = mercadoPagoService;
        }


        [HttpPost("crear-preferencia")]
        [Authorize] // opcional, si querés que solo usuarios logueados puedan pagar
        public async Task<IActionResult> CrearPreferencia([FromBody] CrearPreferenciaRequest request)
        {
            try
            {
                var initPoint = await _mercadoPagoService.CrearPreferenciaAsync(request.Titulo, request.Precio, request.Cantidad);
                return Ok(new { url = initPoint });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error al crear preferencia: {ex.Message}");
            }
        }
    }

    public class CrearPreferenciaRequest
    {
        public string Titulo { get; set; } = "";
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
