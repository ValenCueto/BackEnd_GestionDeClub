using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Http.HttpResults;
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


        [HttpDelete]
        public IActionResult DeleteUser([FromBody] int userId)
        {
            try
            {
                _userService.DeleteUser(userId);
                return Ok($"user {userId} eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateUserState")]
        public IActionResult DeactivateUser([FromBody] int userId)
        {
            try
            {
                _userService.DeactivateUser(userId);
                return Ok($"user {userId} eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
