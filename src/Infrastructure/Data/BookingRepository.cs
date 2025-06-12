using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Models.Response.HourUsageDtoResponse;

namespace Infrastructure.Data
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }
            
        public List<Booking> GetAllBookings()
        {
            return _dbContext.Bookings
                .Include(b => b.Court)
                .Include(b => b.User)
                .ToList();
        }

        public List<(int Hour, int Count)> GetMostFrequentBookingHours()
        {
            return _dbContext.Bookings
                .Where(b => !b.Available)
                .GroupBy(b => b.StartTime.Hour)
                .Select(g => new
                {
                    Hour = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .AsEnumerable() 
                .Select(x => (x.Hour, x.Count))
                .ToList();
        }

    }
}
