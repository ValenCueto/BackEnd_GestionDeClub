using Domain.Enums;

namespace Application.Models.Request
{
    public class UserCreateRequest
    {
        public string Name {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public Rol Rol { get; set; }
    }
}
