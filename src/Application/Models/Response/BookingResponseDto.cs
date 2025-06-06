using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Response
{
    public class BookingResponseDto
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime FinishTime { get; set; }

        public bool Available { get; set; }

        public string? UserEmail { get; set; }

        public int CourtId { get; set; }

        public string UserName { get; set; }

        public static BookingResponseDto Create(Booking booking)
        {
            var dto = new BookingResponseDto
            {
                Id = booking.Id,
                StartTime = booking.StartTime,
                FinishTime = booking.FinishTime,
                Available = booking.Available,
                UserEmail = booking.User?.Email,
                CourtId = booking.Court.Id,
                UserName = booking.User?.Name,

            };
            return dto;
        }
    }
}