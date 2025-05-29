using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ICourtRepository _courtRepository;

        public BookingService(IBookingRepository bookingRepository, ICourtRepository courtRepository, IAvailabilityRepository availabilityRepository)
        {
            _bookingRepository = bookingRepository;
            _courtRepository = courtRepository;
            _availabilityRepository = availabilityRepository;
        }

        public void CreateBooking(BookingCreateRequest request)
        {
            List<DateTime> dateList = new List<DateTime>();
            List<Court> courts = _courtRepository.GetAll();
            List<Booking> bookings = _bookingRepository.GetAll();
            DateTime startTime = request.StartTime;
            DateTime finishTime = request.FinishTime;

            for (DateTime date = startTime; date <= finishTime; date = date.AddDays(1))
            {
                dateList.Add(date);
            }

            foreach (DateTime date in dateList)
            {
                DayOfWeek dayOfWeek = date.DayOfWeek;
                Availability? availability = _availabilityRepository.GetByDay(dayOfWeek) ?? throw new Exception("disponibilidad no existente");

                for (TimeOnly time = availability.StartTime; time.AddMinutes(availability.Duration) <= availability.FinishTime; time = time.AddMinutes(availability.Duration))
                {
                    foreach (Court court in courts)
                    {
                        DateTime start = date.Date + time.ToTimeSpan();
                        DateTime end = start.AddMinutes(availability.Duration);
                        
                        bool bookingExist = bookings.Any(b =>
                            b.Court.Id == court.Id &&
                            b.StartTime == start && 
                            b.FinishTime == end
                        );
                        
                        if (bookingExist == false)
                        {
                            Booking booking = new Booking(start, end, court);
                            court.AddBooking(booking);
                            _bookingRepository.Add(booking);
                        }
                    }
                }
            }
        }

        public List<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings(); 
        }
    }
}