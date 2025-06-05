using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
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
        private readonly IBookingService _bookingService;
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
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("[Action]")]
        public IActionResult UpdateCurrentUser([FromBody] UserCurrentDtoResponse request)
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                _userService.UpdateCurrentUser(request, userId);
                return Ok("Usuario editado exitosamente");
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin,Gerente")]
        [HttpPut("UpdateUser/{userId}")]
        public IActionResult UpdateUser([FromBody] UserUpdateRequest request, [FromRoute] int userId)
        {
            try
            {
                _userService.UpdateUser(request, userId);
                return Ok("Usuario editado exitosamente");
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Client")]
        [HttpDelete("DeleteUser/current")]
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

        [Authorize(Roles = "Admin,Gerente")]
        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser([FromRoute] int userId)
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

        [Authorize(Roles = "Client")]
        [HttpPut("UpdateUserState/current")]
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
        [HttpPut("UpdateUserState/{userId}")]
        public IActionResult DeactivateUser([FromRoute] int userId)
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

        [Authorize(Roles = "Client,Admin")]
        [HttpGet("[Action]")]
        public IActionResult GetCurrentUser()
        {
            var userId = GetAuthenticatedUserId();
            return Ok(_userService.GetCurrent(userId));
        }

        [Authorize(Roles = "Client,Admin")]
        [HttpPut("AssignBooking/{bookingId}")]
        public IActionResult AssignBooking(int bookingId)
        {
            int userId = GetAuthenticatedUserId();
            _userService.AssignBooking(bookingId, userId);
            return Ok();
        }

        [Authorize(Roles = "Client,Admin")]
        [HttpPut("[Action]")]
        public IActionResult MarkUserPaid()
        {
            int userId = GetAuthenticatedUserId();
            _userService.MarkUserPaid(userId);
            return Ok();
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet("[Action]")]
        public IActionResult GetActivesUsers()
        {
                return Ok(_userService.GetActivesUsers());
        }
    }
}
