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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void CreateUser(UserCreateRequest request)
        {
            var newUser = new User(
            request.Name,
            request.Email,
            request.Password,
            request.PhoneNumber,
            request.Rol
        );

            _userRepository.Add(newUser);
            //return UserDtoResponse.Create(newUser);
        }

        public void DeleteUser(int userId)
        {
            User userToDelete = _userRepository.GetById(userId) ?? throw new KeyNotFoundException($"El user {userId} no fué encontrado");
            _userRepository.Delete(userToDelete);
        }

        public void DeactivateUser(int userId)
        {
            User userToDeactivate = _userRepository.GetById(userId) ?? throw new KeyNotFoundException($"El user {userId} no fué encontrado");
            userToDeactivate.State = false;
            _userRepository.Update(userToDeactivate);
        }


    }
}
