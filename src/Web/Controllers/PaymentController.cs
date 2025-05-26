using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpPost("assign-all/{monthlyFeeId}")]
        public IActionResult AssignToAll(int monthlyFeeId)
        {
            _service.AssignFeeToAllUsers(monthlyFeeId);
            return Ok("Cuotas asignadas a todos los usuarios");
        }

        [HttpPost("assign-one")]
        public IActionResult AssignToUser([FromBody] MarkPaymentRequest request)
        {
            _service.AssignFeeToUser(request.UserId, request.MonthlyFeeId);
            return Ok("Cuota asignada al usuario");
        }

        [HttpPut("mark-paid")]
        public IActionResult MarkAsPaid([FromBody] MarkPaymentRequest request)
        {
            _service.MarkAsPaid(request);
            return Ok("Pago registrado");
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            return Ok(_service.GetByUserId(userId));
        }
    }
}
