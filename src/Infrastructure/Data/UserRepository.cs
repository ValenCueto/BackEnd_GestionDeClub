using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public User? GetByName(string userName)
        {
            return _dbContext.Users
                .FirstOrDefault(u => u.Email == userName || u.Name == userName);
        }

        public User? GetByEmail(string email)
        {
            return _dbContext.Users
                .FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<User?> GetByResetTokenAsync(string token)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u =>
                u.ResetPasswordToken == token &&
                u.ResetPasswordTokenExpiry > DateTime.UtcNow);
        }

        public List<User> GetActiveUsers()
        {
            return _dbContext.Users.Where(u => u.State == true).ToList();
        }
    }
}
