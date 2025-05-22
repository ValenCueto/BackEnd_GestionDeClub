using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
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
    }
}
