using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserCreateRequest request);
        void UpdateUser(UserUpdateRequest request, int userId);
        void UpdateCurrentUser(UserCurrentDtoResponse request, int userId);
        void DeleteUser(int userId);
        void DeactivateUser(int userId);
        List<UserDtoResponse> GetAll();
        UserCurrentDtoResponse GetCurrent(int userId);
        void AssignBooking(int bookingId, int userId);
    }
}
