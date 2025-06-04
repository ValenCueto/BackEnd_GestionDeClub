using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Authorize(Roles = "Admin,Gerente")]
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyFeeController : ControllerBase
    {
        private readonly IMonthlyFeeService _service;

        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : -1;
        }
        public MonthlyFeeController(IMonthlyFeeService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MonthlyFeeCreateRequest request)
        {
            _service.Create(request);
            return Ok("Cuota mensual creada");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{month}/{year}")]
        public IActionResult GetByMonthYear(int month, int year)
        {
            var fee = _service.GetByMonthYear(month, year);
            return fee == null ? NotFound() : Ok(fee);
        }

        [Authorize]
        [HttpPut("[Action]/{monthlyFeeId}")]
        public IActionResult UpdateMonthlyFee([FromBody] MonthlyFeeUpdateRequest request, int monthlyFeeId)
        {
         
             _service.Update(request, monthlyFeeId);
              return Ok("Cuota editada exitosamente");  
        }

        [Authorize]
        [HttpDelete("[Action]/{monthlyFeeId}")]
        public IActionResult DeleteMonthlyFee(int monthlyFeeId)
        {
           _service.Delete(monthlyFeeId);
            return Ok();
        }



    }

}
