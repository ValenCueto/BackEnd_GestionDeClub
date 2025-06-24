using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        List<Booking> GetAllBookings();
        List<(int Hour, int Count)> GetMostFrequentBookingHours();
        int CountBookingsByMonth(int month, int year);
    }
}
