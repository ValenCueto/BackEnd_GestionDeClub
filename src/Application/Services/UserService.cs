using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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
        }

        public void UpdateUser(UserCreateRequest request, int userId)
        {
            User userToEdit = _userRepository.GetById(userId) ?? throw new KeyNotFoundException($"El usuario con ID {userId} no fué encontrado");
            userToEdit.Name = request.Name;
            userToEdit.Email = request.Email;
            userToEdit.Password = request.Password;
            userToEdit.PhoneNumber = request.PhoneNumber;
            userToEdit.Rol = request.Rol;

            _userRepository.Update(userToEdit);
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

        public List<UserDtoResponse> GetAll()
        {
            List<User> users = _userRepository.GetAll() ?? throw new Exception("No hay usuarios");
            List<UserDtoResponse> usersFiltered = new List<UserDtoResponse>();
            foreach (User user in users)
            {
                UserDtoResponse userFiltered =  UserDtoResponse.Create(user);
                usersFiltered.Add(userFiltered);
            }

            return usersFiltered;
        }
    }
}
