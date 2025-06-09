using Application.Interfaces;
using Application.Models.Request;
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

       
    }

}
