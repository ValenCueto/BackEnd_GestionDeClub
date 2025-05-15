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
            List<DateTime> dateList = new List<DateTime>(); // lista de fechas del dia X hasta el dia Y
            List<Court> courts = _courtRepository.GetAll();
            DateTime startTime = request.StartTime;
            DateTime finishTime = request.FinishTime;

            for (DateTime date = startTime; date <= finishTime; date = date.AddDays(1))
            {
                dateList.Add(date);
            }

            //falta validar que no se repitan fecha, mes, año y cancha
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

                        Booking booking = new Booking(start, end, court);
                        _bookingRepository.Add(booking);
                        court.AddBooking(booking);
                    }
                }
            }
        }
    }
}




//DateTime startDate = request.StartTime.Date;
//DateTime endDate = request.FinishTime.Date;
//for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
//{
//    var dayOfWeek = date.DayOfWeek;

//    var availability = _availabilityRepository.GetByDay(dayOfWeek);
//    if (availability == null)
//        continue; // Si no hay disponibilidad ese día, lo salteamos

//    foreach (var court in courts)
//    {
//        for (var time = availability.StartTime;
//                 time.AddMinutes(availability.Duration) <= availability.FinishTime;
//                 time = time.AddMinutes(availability.Duration))
//        {
//            DateTime bookingStart = date.Add(time.ToTimeSpan());
//            DateTime bookingEnd = date.Add(time.AddMinutes(availability.Duration).ToTimeSpan());

//            Booking booking = new Booking(bookingStart,bookingEnd,court)
//            {
//                StartTime = bookingStart,
//                FinishTime = bookingEnd,
//                Available = true,
//                User = null // o el usuario correspondiente
//            };

//            _bookingRepository.Add(booking);
//            court.AddBooking(booking);
//        }
//    }
//}