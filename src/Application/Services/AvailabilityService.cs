using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityRepository _availabilityRepository;
        public AvailabilityService(IAvailabilityRepository availabilityRepository)
        {
            _availabilityRepository = availabilityRepository;
        }

        public void InitDefaultAvailability(AvailabilityInitRequest dto)
        {
            var existingAvailabilities = _availabilityRepository.GetAll();

            if (existingAvailabilities != null && existingAvailabilities.Any())
            {
                throw new BadRequestException("Ya existe disponibilidad cargada.");
            }

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                var availability = new Availability
                {
                    DayOfWeek = day,
                    StartTime = dto.StartTime,
                    FinishTime = dto.FinishTime,
                    Duration = dto.Duration
                };

                _availabilityRepository.Add(availability);
            }
        }

        public List<AvailabilityResponseDto> GetAll()
        {
            var availabilities = _availabilityRepository.GetAll();
            var response = new List<AvailabilityResponseDto>();

            foreach (var availability in availabilities)
            {
                var dto = new AvailabilityResponseDto();
                dto.DayOfWeek = availability.DayOfWeek.ToString();
                dto.StartTime = availability.StartTime;
                dto.FinishTime = availability.FinishTime;
                dto.Duration = availability.Duration;

                response.Add(dto);
            }

            return response;
        }

        public void UpdateAvailability(AvailabilityInitRequest dto, string day)
        {
            var availabilityToEdit = _availabilityRepository.GetByDay(day);
            availabilityToEdit.StartTime = dto.StartTime;
            availabilityToEdit.FinishTime = dto.FinishTime;
            availabilityToEdit.Duration = dto.Duration;
            _availabilityRepository.Update(availabilityToEdit);
        }
    }
}
