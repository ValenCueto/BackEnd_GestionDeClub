using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        public UserService(IUserRepository userRepository, IBookingRepository bookingRepository)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
        }
        public void CreateUser(UserCreateRequest request)
        {
            var existingUserEmail = _userRepository.GetByEmail(request.Email);
            if (existingUserEmail is not null)
            {
                throw new BadRequestException("Ya existe un usuario con ese email");
            }
            var newUser = new User(
            request.Name,
            request.Email,
            request.Password,
            request.PhoneNumber,
            request.SubscriptionId     
        );
            _userRepository.Add(newUser);
        }

        public void UpdateUser(UserUpdateRequest request, int userId)
        {
            User userToEdit = _userRepository.GetById(userId) ?? throw new KeyNotFoundException($"El usuario con ID {userId} no fué encontrado");
            var existingUserEmail = _userRepository.GetByEmail(request.Email);
            if (existingUserEmail is not null && userToEdit.Id != userId)
            {
                throw new BadRequestException("Ya existe un usuario con ese email");
            }
            userToEdit.Name = request.Name;
            userToEdit.Email = request.Email;
            userToEdit.PhoneNumber = request.PhoneNumber;
            userToEdit.Rol = request.Rol;
            userToEdit.SubscriptionId = request.SubscriptionId;
            userToEdit.State = request.State;

            _userRepository.Update(userToEdit);
        }

        public void UpdateCurrentUser(UserCurrentDtoResponse request, int userId)
        {
            User userToEdit = _userRepository.GetById(userId) ?? throw new KeyNotFoundException($"El usuario con ID {userId} no fué encontrado");
            var existingUserEmail = _userRepository.GetByEmail(request.Email);
            if (existingUserEmail is not null && userToEdit.Id != userId)
            {
                throw new BadRequestException("Ya existe un usuario con ese email");
            }
            userToEdit.Name = request.Name;
            userToEdit.Email = request.Email;
            userToEdit.PhoneNumber = request.PhoneNumber;
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

        public UserCurrentDtoResponse GetCurrent(int userId)
        {
            var user = _userRepository.GetById(userId);
            var userToResponse = new UserCurrentDtoResponse()
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            return userToResponse;
        }
        
        public void AssignBooking(int bookingId, int userId)
        {
            User user = _userRepository.GetById(userId) ?? throw new Exception("User not Found");
            Booking booking = _bookingRepository.GetById(bookingId) ?? throw new Exception("Booking not Found");
            user.AddBooking(booking);
            booking.User = user;
            booking.Available = false;
            _bookingRepository.Update(booking);
        }

        public void MarkUserPaid(int userId) 
        {
            User user = _userRepository.GetById(userId);
            user.Paid = true;
            _userRepository.Update(user);
        }

        public List<UserCurrentDtoResponse> GetActivesUsers()
        {
            var usersActives = _userRepository.GetActiveUsers();
            var usersActivesList = new List<UserCurrentDtoResponse>();
            foreach (var user in usersActives)
            {
                usersActivesList.Add(new UserCurrentDtoResponse
                {
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                });
            }
            return usersActivesList;
        }
    }
}
