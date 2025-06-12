using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost("CreateBooking")]
        public IActionResult CreateBooking([FromBody] BookingCreateRequest request)
        {
            _bookingService.CreateBooking(request);
            return Ok();
            //CreatedAtAction()            
        }
        [HttpGet("GetAllBookings")]
        public IActionResult GetAllBookings()
        {
            List<BookingResponseDto> bookings = _bookingService.GetAllBookings();
            return Ok(bookings);
        }

        [Authorize(Roles = "Gerente")]
        [HttpGet("[Action]")]
        public IActionResult GetMostFrequentBookingHours()
        {
                return Ok(_bookingService.GetMostFrequentBookingHours());
        }
    }
}
