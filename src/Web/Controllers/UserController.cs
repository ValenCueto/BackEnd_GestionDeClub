using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequest request)
        {
            var result = _userService.CreateUser(request);
            return Ok(result);
        }
    }
}
