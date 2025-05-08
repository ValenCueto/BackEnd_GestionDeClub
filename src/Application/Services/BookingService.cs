using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }
        public void CreateBooking(BookingCreateRequest request)
        {
            List<DateTime> dateList = new List<DateTime>();
            for (DateTime date = request.StartTime; date <= request.FinishTime; date.AddDays(1)) 
            { 
                dateList.Add(date);
            }

            foreach (DateTime date in dateList) 
            {
                var dayOfWeek = date.DayOfWeek;

                var availabilityOfDay = _availabilityRepository.GetByDay(dayOfWeek.ToString());
                
            }
        }
    }
}
