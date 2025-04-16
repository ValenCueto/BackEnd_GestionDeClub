using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserDtoResponse CreateUser(UserCreateRequest request)
        {
            User newUser = new User(request.Name, request.Email, request.Password , request.PhoneNumber, request.Rol);
            _userRepository.Add(newUser);
            return UserDtoResponse.Create(newUser);
        }

 
    }
}
