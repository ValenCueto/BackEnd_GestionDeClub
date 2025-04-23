using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
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
            try
            {
                _userService.CreateUser(request);
                return Ok("Usuario creado exitosamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

         [HttpPost("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserCreateRequest request, int userId)
        {
            try 
            {
                _userService.UpdateUser(request,userId);
                return Ok("Usuario editado exitosamente");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
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
