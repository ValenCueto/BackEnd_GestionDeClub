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
        void UpdateUser(UserCreateRequest request, int userId);
        void DeleteUser(int userId);
        void DeactivateUser(int userId);
        List<UserDtoResponse> GetAll();
    }
}
