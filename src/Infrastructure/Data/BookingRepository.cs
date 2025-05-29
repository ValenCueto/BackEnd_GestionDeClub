using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
    }
}
