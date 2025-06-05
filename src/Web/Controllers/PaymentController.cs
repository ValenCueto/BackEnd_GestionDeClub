using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : -1;
        }
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

        [HttpGet("[Action]")]
        public IActionResult GetByCurrentUser()
        {
            var userId = GetAuthenticatedUserId();
            return Ok(_service.GetByUserId(userId));
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet("[Action]")]
        public IActionResult GetCurrentMonthRevenue()
        {
                return Ok(_service.GetCurrentMonthRevenue());
        }
    }
}
