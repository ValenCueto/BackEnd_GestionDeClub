using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IBookingService
    {
        void CreateBooking(BookingCreateRequest request);
        List<BookingResponseDto> GetAllBookings();
        List<HourUsageDtoResponse> GetMostFrequentBookingHours();
    }
}
