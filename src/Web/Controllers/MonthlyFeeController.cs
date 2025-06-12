using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyFeeController : ControllerBase
    {
        private readonly IMonthlyFeeService _service;

        public MonthlyFeeController(IMonthlyFeeService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Admin,Gerente")]
        [HttpPost]
        public IActionResult Create([FromBody] MonthlyFeeCreateRequest request)
        {
            var newFee = _service.Create(request);
            return Ok(newFee);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [Authorize]
        [HttpGet("{month}/{year}")]
        public IActionResult GetByMonthYear(int month, int year)
        {
            var fee = _service.GetByMonthYear(month, year);
            return fee == null ? NotFound() : Ok(fee);
        }

        [Authorize(Roles = "Admin,Gerente")]
        [HttpPut("[Action]/{monthlyFeeId}")]
        public IActionResult UpdateMonthlyFee([FromBody] MonthlyFeeUpdateRequest request, int monthlyFeeId)
        {
         
             _service.Update(request, monthlyFeeId);
              return Ok("Cuota editada exitosamente");  
        }

        [Authorize(Roles = "Admin,Gerente")]
        [HttpDelete("[Action]/{monthlyFeeId}")]
        public IActionResult DeleteMonthlyFee(int monthlyFeeId)
        {
           _service.Delete(monthlyFeeId);
            return Ok();
        }



    }

}
