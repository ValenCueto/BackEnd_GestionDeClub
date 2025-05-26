using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Court
    {
        [Key]
        public int Id { get; set; }

        private List<Booking> _bookings = new List<Booking>();

        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
        }

        public void DeleteBooking(Booking booking) 
        {
            _bookings.Remove(booking);
        }

        public List<Booking> GetBookings() 
        { 
            return _bookings; 
        }
    }
}