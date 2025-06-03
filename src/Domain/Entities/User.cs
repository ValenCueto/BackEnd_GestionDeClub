using Domain.Enums;

namespace Domain.Entities
{   

    public class User
    {
        private List<Booking> _bookings = new List<Booking>();
        public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();
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
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }

        public User(string name, string email, string password, int phoneNumber, int? subscriptionId)
        {
            Name = name;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            SubscriptionId = subscriptionId;
            Rol = Rol.Client;
            State = true;
        }

        public void AddBooking(Booking booking)
        {
            _bookings.Add(booking);
        }
        
    }
}
