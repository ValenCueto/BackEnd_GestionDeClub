using Application.Interfaces;
using Application.Models.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
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

        [Authorize(Roles = "Admin,Gerente")]
        [HttpPost("assign-all/{monthlyFeeId}")]
        public IActionResult AssignToAll(int monthlyFeeId)
        {
            _service.AssignFeeToAllUsers(monthlyFeeId);
            return Ok();
        }

        [Authorize(Roles = "Admin,Gerente")]
        [HttpPost("assign-one")]
        public IActionResult AssignToUser([FromBody] MarkPaymentRequest request)
        {
            _service.AssignFeeToUser(request.UserId, request.MonthlyFeeId);
            return Ok();
        }

        [Authorize]
        [HttpPut("mark-paid")]
        public IActionResult MarkAsPaid([FromBody] MarkPaymentRequest request)
        {
            _service.MarkAsPaid(request);
            return Ok();
        }

        [Authorize(Roles = "Admin,Gerente")]
        [HttpGet("user/{userId}")]
        public IActionResult GetByUser(int userId)
        {
            return Ok(_service.GetByUserId(userId));
        }

        [Authorize]
        [HttpGet("[Action]")]
        public IActionResult GetByCurrentUser()
        {
            var userId = GetAuthenticatedUserId();
            return Ok(_service.GetByUserId(userId));
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet("[Action]")]
        public IActionResult GetMonthlyRevenue(int month, int year)
        {
                return Ok(_service.GetMonthlyRevenue(month, year));
        }
    }
}
