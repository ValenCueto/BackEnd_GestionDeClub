using System.Security.Claims;
using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IUserService _userService;

        public BookingController(IBookingService bookingService,IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : -1;
        }

        [HttpPost("CreateBooking")]
        public IActionResult CreateBooking([FromBody] BookingCreateRequest request)
        {
            try
            {
                _bookingService.CreateBooking(request);
                return Ok();
                //CreatedAtAction()
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("GetAllBookings")]
        public IActionResult GetAllBookings()
        {
            List<BookingResponseDto> bookings = _bookingService.GetAllBookings();
            return Ok(bookings);
        }
    }
}
