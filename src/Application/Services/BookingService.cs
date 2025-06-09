using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IAvailabilityRepository _availabilityRepository;
        private readonly ICourtRepository _courtRepository;
        private readonly IUserRepository _userRepository;

        public BookingService(IBookingRepository bookingRepository, ICourtRepository courtRepository, IAvailabilityRepository availabilityRepository, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _courtRepository = courtRepository;
            _availabilityRepository = availabilityRepository;
            _userRepository = userRepository;
        }

        public void CreateBooking(BookingCreateRequest request)
        {
            List<DateTime> dateList = new List<DateTime>();
            List<Court> courts = _courtRepository.GetAll();
            if(courts.Count == 0 || courts == null) 
            {
                throw new NotFoundException("No hay canchas disponibles.");
            }

            if(request.StartTime > request.FinishTime)
            {
                throw new BadRequestException("La fecha de inicio no puede ser mayor a la de finalización.");
            }

            if(request.StartTime.Date < DateTime.Today)
            {
                throw new BadRequestException("No se pueden crear reservas en fechas pasadas.");
            }

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
                Availability? availability = _availabilityRepository.GetByDay(dayOfWeek);
                if (availability == null)
                {
                    throw new NotFoundException("disponibilidad no existente");
                }

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

        public List<BookingResponseDto> GetAllBookings()
        {
            List<Booking> bookings = _bookingRepository.GetAllBookings();
            return bookings.Select(BookingResponseDto.Create).ToList();
        }

        public List<HourUsageDtoResponse> GetMostFrequentBookingHours()
        {
            var mostFrequentHours = _bookingRepository.GetMostFrequentBookingHours();
            var responseList = new List<HourUsageDtoResponse>();

            foreach (var hour in mostFrequentHours)
            {
                responseList.Add(new HourUsageDtoResponse
                {
                    Hour = hour.Hour,
                    Count = hour.Count
                });
            }
            return responseList;
        }



    }
}