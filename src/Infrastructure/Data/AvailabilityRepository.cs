using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AvailabilityRepository : BaseRepository<Availability>, IAvailabilityRepository
    {
        public AvailabilityRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Availability GetByDay(string day) 
        {
            return _dbContext.Availabilities.FirstOrDefault(a => a.DayOfWeek.ToString().ToLower() == day.ToLower());
        }
    }
}
