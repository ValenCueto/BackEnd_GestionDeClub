using Domain.Enums;

namespace Application.Models.Request
{
    public class UserCreateRequest
    {
        public int? SubscriptionId { get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Rol Rol { get; set; }
    }
}
