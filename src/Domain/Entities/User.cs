using Domain.Enums;

namespace Domain.Entities
{   

    public class User
    {
        private List<Booking> _bookings;

        public int Id { get; set; }
        public int? SubscriptionId { get; set; }
        public string Name { get; set; }
        public Rol Rol { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public bool State { get; set; }
        public bool Paid { get; set; }
        public Subscription Subscription { get; set; }
        public IReadOnlyCollection<Booking> Bookings => _bookings;

        public User(string name, string email, string password, int phoneNumber, Rol rol, int? subscriptionId)
        {
            Name = name;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Rol = rol;
            SubscriptionId = subscriptionId;
        }



        public void AddBooking(Booking booking)
        {
            //Reglas de negocios
            //Validar estado, subscripcion

            //Bookings.Add(booking);
        }
        
    }
}
