using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Request;
using Domain.Entities;
using Domain.Interfaces;

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
                Availability? availability = _availabilityRepository.GetByDay(dayOfWeek.ToString()) ?? throw new Exception("disponibilidad no existente");

                for (var time = availability.StartTime; time < availability.FinishTime; time = time.AddMinutes(availability.Duration))
                {
                    foreach (Court court in courts)
                    {
                        Booking booking = new Booking(startTime,finishTime,court);
                        _bookingRepository.Add(booking);
                        court.AddBooking(booking);
                    }
                }
            }
        }
    }
}
