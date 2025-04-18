using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateRequest request)
        {
             _userService.CreateUser(request);
            return Ok("ok");
        }

        [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserCreateRequest request, int userId)
        {
            try
            {
                _userService.UpdateUser(request, userId);
                return Ok("Usuario editado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }





    }
}
