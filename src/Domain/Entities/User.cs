using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{   

    public class User
    {
        private List<Booking> _bookings;
        public int Id { get; set; }
        public string Name { get; set; }
        public Rol Rol { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public bool State { get; set; }
        public Subscription Subscription { get; set; }
        public IReadOnlyCollection<Booking> Bookings => _bookings;
        public void AddBooking(Booking booking)
        {
            //Reglas de negocios
            //Validar estado, subscripcion

            Bookings.Add(booking);
        }
        
    }
}
