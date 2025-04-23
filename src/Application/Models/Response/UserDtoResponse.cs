using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Response
{
    public class UserDtoResponse
    {
        public string Name {  get; set; }
        public Rol Rol { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public bool State {  get; set; }
        public Subscription Subscription { get; set; }

        public static UserDtoResponse Create(User user)
        {
            var dto = new UserDtoResponse { Name = user.Name,
                Rol = user.Rol,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                State = user.State
            };

            return dto;
        }
    }
}
