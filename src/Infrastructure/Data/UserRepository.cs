using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;

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
    }
}
