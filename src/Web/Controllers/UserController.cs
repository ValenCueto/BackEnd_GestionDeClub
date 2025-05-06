using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : -1;
        }

        [HttpPost("CreateUser")]
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

        [Authorize(Roles = "Client")]
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserCreateRequest request)
        {
            try 
            {
                var userId = GetAuthenticatedUserId();
                _userService.UpdateUser(request,userId);
                return Ok("Usuario editado exitosamente");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }

        [Authorize(Roles = "Client")]
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                _userService.DeleteUser(userId);
                return Ok($"user {userId} eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Client")]
        [HttpPut("UpdateUserState")]
        public IActionResult DeactivateUser()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                _userService.DeactivateUser(userId);
                return Ok($"user {userId} eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin,Gerente")]
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                List<UserDtoResponse> users = _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex);
            }
        }
    }
}
